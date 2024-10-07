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

    private readonly string _HotelRoomQuery
        = @"SELECT hr.*, h.*, a.*, r.* FROM HotelRoom hr
            INNER JOIN Hotel h ON h.IdHotel = hr.IdHotel
            INNER JOIN Address a ON a.IdAddress = hr.IdAddress
            INNER JOIN Room r ON r.IdRoom = hr.IdRoom";
    private readonly string _HotelRoomDelete
        = @"DELETE FROM HotelRoom WHERE IdHotel = @unIdhotel AND Number = @unRoomNumber";

    public async Task<List<HotelRoom>> GetHotelRooms()
    {
        var hotelroom = (await _connection.QueryAsync<HotelRoom, Hotel, Address, Room, HotelRoom>(_HotelRoomQuery,
            (hotelroom, hotel, address, room) =>
            {
                hotelroom.Hotel = hotel;
                hotelroom.Address = address;
                hotelroom.Room = room;
                return hotelroom;
            },
            splitOn: "IdHotel, IdAddress, IdRoom"
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
    public async Task DeleteHotelRoom(int IdHotel, int RoomNumber)
    {
        await _connection.ExecuteAsync(_HotelRoomDelete, new { unIdHotel = IdHotel, unRoomNumber = RoomNumber });
    }

    public static DynamicParameters ParametersHotelRoom(HotelRoom hotelRoom)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@unIdHotel", hotelRoom.Hotel!.IdHotel);
        parameters.Add("@unIdAddress", hotelRoom.Address!.IdAddress);
        parameters.Add("@unIdRoom", hotelRoom.Room!.IdRoom);

        return parameters;
    }
}