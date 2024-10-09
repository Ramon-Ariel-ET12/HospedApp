using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories;

public class RoomDapper
{
    private readonly IDbConnection _connection;
    public RoomDapper(IDbConnection connection) => _connection = connection;

    private readonly string _RoomQuery
        = @"SELECT * FROM Room";
    private readonly string _RoomDelete
        = @"DELETE FROM Room WHERE IdRoom = @unIdRoom";
    public List<Room> GetRooms()
    {
        var room = _connection.Query<Room>(_RoomQuery).ToList();
        return room;
    }
    public void CreateRoom(Room room)
    {
        var parameters = ParametersRoom(room);
        try
        {
            _connection.Execute("RegisterRoom", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            throw new ConstraintException(ex.Message);
        }
    }
    public void ModifyRoom(Room room)
    {
        var parameters = ParametersRoom(room);
        try
        {
            _connection.Execute("ModifyRoom", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            throw new ConstraintException(ex.Message);
        }
    }
    public void DeleteRoom(int IdRoom)
    {
        _connection.Execute(_RoomDelete, new { unIdRoom = IdRoom});
    }

    private static DynamicParameters ParametersRoom(Room room)
    {
        var parameters = new DynamicParameters();
        if (room.IdRoom != 0)
            parameters.Add("@unIdRoom", room.IdRoom);
        parameters.Add("@unGarage", room.Garage);
        parameters.Add("@unPriceNight", room.PriceNight);
        parameters.Add("@unDescription", room.Description);
        
        return parameters;
    }
}
