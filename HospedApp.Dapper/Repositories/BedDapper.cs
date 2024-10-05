using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories;

public class BedDapper
{
    private IDbConnection _connection;
    public BedDapper(IDbConnection connection) => _connection = connection;

    private readonly string _BedQuery
        = @"SELECT * FROM Bed";
    private readonly string _BedDelete
        = @"DELETE FROM Bed WHERE IdBed = @unIdBed";
    public List<Bed> GetBeds()
    {
        var bed = _connection.Query<Bed>(_BedQuery).ToList();
        return bed;
    }
    public void CreateBed(Bed bed)
    {
        var parameters = ParametersBed(bed);

        try
        {
            _connection.Execute("RegisterBed", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            if (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (ex.Message.Contains("Name"))
                    throw new ConstraintException($"El nombre {bed.Name} ya existe");

            }
        }

    }
    public void DeleteBed(int IdBed)
    {
        _connection.Execute(_BedDelete, new { unIdBed = IdBed });
    }

    private static DynamicParameters ParametersBed(Bed bed)
    {
        var parameters = new DynamicParameters();
        if (bed.IdBed == 0)
            parameters.Add("@unIdBed", bed.IdBed);
        parameters.Add("@unName", bed.Name);
        parameters.Add("@unCanSleep", bed.CanSleep);

        return parameters;
    }
}
