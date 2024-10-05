using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using HospedApp.Core.Entities.Relations;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories;

public class RoomBedDapper
{
    private readonly IDbConnection _connection;
    public RoomBedDapper(IDbConnection connection) => _connection = connection;

    private readonly string _RoomBedQuery
        = @"SELECT * FROM RoomBed rb
            INNER JOIN Room r ON r.IdRoom = rb.IdRoom
            INNER JOIN Bed b ON b.IdBed = rb.IdBed";
    private readonly string _RoomBedDelete
        = @"DELETE FROM RoomBed WHERE IdRoom = @unIdRoom AND IdBed = @unIdBed";

    public List<RoomBed> GetRoomBeds()
    {
        var roombed = _connection.Query<RoomBed, Room, Bed, RoomBed>(_RoomBedQuery,
            (roombed, room, bed) => 
            {
                roombed.Room = room;
                roombed.Bed = bed;
                return roombed;
            },
            splitOn: "IdRoom, IdBed"
            ).ToList();
        return roombed;
    }
    public void CreateRoomBed(RoomBed roomBed)
    {
        var parameters = ParametersRoomBed(roomBed);
        try
        {
            _connection.Execute("RegisterRoomBed", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            throw new ConstraintException(ex.Message);;
        }
    }
    public void DeleteRoomBed(int IdRoom, int IdBed)
    {
        _connection.Execute(_RoomBedDelete, new { unIdRoom = IdRoom, unIdBed = IdBed });
    }

    public static DynamicParameters ParametersRoomBed(RoomBed roomBed)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@unIdRoom", roomBed.Room!.IdRoom);
        parameters.Add("@unIdBed", roomBed.Bed!.IdBed);
        parameters.Add("@unBedQuantity", roomBed.BedQuantity);

        return parameters;
    }
}