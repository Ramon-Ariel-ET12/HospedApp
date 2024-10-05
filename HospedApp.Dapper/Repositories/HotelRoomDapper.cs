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
        = @"SELECT hr.*, h.*, r.* FROM HotelRoom hr
            INNER JOIN Hotel h ON h.IdHotel = hr.IdHotel
            INNER JOIN Room r ON r.IdRoom = hr.IdRoom";
    private readonly string _HotelRoomDelete
        = @"DELETE FROM HotelRoom WHERE Number = @unRoomNumber";

    public List<HotelRoom> GetHotelRooms()
    {
        var hotelroom = _connection.Query<HotelRoom, Hotel, Room, HotelRoom>(_HotelRoomQuery,
            (hotelroom, hotel, room) =>
            {
                hotelroom.Hotel = hotel;
                hotelroom.Room = room;
                return hotelroom;
            },
            splitOn: "IdHotel, IdRoom"
        ).ToList();
        return hotelroom;
    }
    public void CreateHotelRoom(HotelRoom hotelRoom)
    {
        var parameters = ParametersHotelRoom(hotelRoom);
        try
        {
            _connection.Execute("RegisterHotelRoom", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            throw new ConstraintException(ex.Message); ;
        }
    }
    public void DeleteHotelRoom(int RoomNumber)
    {
        _connection.Execute(_HotelRoomDelete, new { unRoomNumber = RoomNumber });
    }

    public static DynamicParameters ParametersHotelRoom(HotelRoom hotelRoom)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@unIdHotel", hotelRoom.Hotel!.IdHotel);
        parameters.Add("@unIdRoom", hotelRoom.Room!.IdRoom);

        return parameters;
    }
}