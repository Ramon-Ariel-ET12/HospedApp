using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using HospedApp.Core.Entities.Relations;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories;

public class HotelRoomDapper
{
    private readonly IDbConnection _connection;
    public HotelRoomDapper(IDbConnection connection) => _connection = connection;
    private readonly string _HotelRoomDelete
        = @"DELETE FROM HotelRoom WHERE IdAddress = @unIdAddress AND Number = @unRoomNumber";

    private readonly string _HotelRoomQuery
        = @"SELECT * FROM HotelRoom hr 
        INNER JOIN Address a ON a.IdAddress = hr.IdAddress 
        INNER JOIN RoomBed ro ON ro.IdRoomBed = hr.IdRoomBed
        INNER JOIN Room r ON r.IdRoom = ro.IdRoom
        INNER JOIN Bed b ON b.IdBed = ro.IdBed
        INNER JOIN Hotel h ON h.IdHotel = a.IdHotel";

    public async Task<List<HotelRoom>> GetHotelRooms()
    {
        var hotelroom = (await _connection.QueryAsync<HotelRoom, Address, RoomBed, Room, Bed, Hotel, HotelRoom>(_HotelRoomQuery,
            (hotelroom, address, roombed, room, bed, hotel) =>
            {
                hotelroom.Address = address;
                hotelroom.RoomBed = roombed;
                hotelroom.RoomBed.Room = room;
                hotelroom.RoomBed.Bed = bed;
                hotelroom.Address.Hotel = hotel;
                return hotelroom;
            },
            splitOn: "IdAddress, IdRoomBed, IdRoom, IdBed, IdHotel"
        )).ToList();
        return hotelroom;
    }
    public async Task CreateHotelRoom(HotelRoom hotelRoom)
    {
        var parameters = ParametersHotelRoom(hotelRoom);
        try
        {
            await _connection.ExecuteAsync("RegisterHotelRoom", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            throw new ConstraintException(ex.Message); ;
        }
    }
    public async Task DeleteHotelRoom(int IdAddress, int RoomNumber)
    {
        await _connection.ExecuteAsync(_HotelRoomDelete, new { unIdAddress = IdAddress, unRoomNumber = RoomNumber });
    }

    public static DynamicParameters ParametersHotelRoom(HotelRoom hotelRoom)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@unIdAddress", hotelRoom.Address!.IdAddress);
        parameters.Add("@unIdRoomBed", hotelRoom.RoomBed!.IdRoomBed);

        return parameters;
    }
}