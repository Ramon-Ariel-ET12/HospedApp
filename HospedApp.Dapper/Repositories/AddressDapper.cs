using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories;

public class AddressDapper
{
    private readonly IDbConnection _connection;
    public AddressDapper(IDbConnection connection) => _connection = connection;

    private readonly string _addressQuery
        = @"SELECT a.*, h.* FROM Address a
            INNER JOIN Hotel h ON a.IdHotel = h.IdHotel";
    private readonly string _addressDelete
        = @"DELETE FROM Address WHERE IdAddress = @unIdAddress";
    public List<Address> GetAddresses()
    {
        var address = _connection.Query<Address, Hotel, Address>(_addressQuery,
            (address, hotel) =>
            {
                address.Hotel = hotel;
                return address;
            },
            splitOn: "IdHotel"
        ).ToList();
        return address;
    }
    public void CreateAddress(Address address)
    {
        var parameters = ParametersAddress(address);

        try
        {
            _connection.Execute("RegisterAddress", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            if (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (ex.Message.Contains("Domicile"))
                    throw new ConstraintException($"El Domicilie {address.Domicile} ya está en uso");
            }
        }
    }
    public void ModifyAddress(Address address)
    {
        var parameters = ParametersAddress(address);

        try
        {
            _connection.Execute("ModifyAddress", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            throw new ConstraintException(ex.Message);
        }
    }
    public void DeleteAddress(int IdAddress)
    {
        _connection.Execute(_addressDelete, new { unIdAddress = IdAddress });
    }

    public static DynamicParameters ParametersAddress(Address address)
    {
        var parameters = new DynamicParameters();

        if (address.IdAddress != 0)
            parameters.Add("@unIdAddress", address.IdAddress);
        parameters.Add("@unIdHotel", address.Hotel!.IdHotel);
        parameters.Add("@unDomicile", address.Domicile);
        parameters.Add("@unPostalCode", address.PostalCode);
        return parameters;
    }
}
