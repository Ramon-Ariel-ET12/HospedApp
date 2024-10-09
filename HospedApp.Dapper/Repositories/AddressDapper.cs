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
    public async Task<List<Address>> GetAddresses()
    {
        var address = (await _connection.QueryAsync<Address, Hotel, Address>(_addressQuery,
            (address, hotel) =>
            {
                address.Hotel = hotel;
                return address;
            },
            splitOn: "IdHotel"
        )).ToList();
        return address;
    }
    public async Task CreateAddress(Address address)
    {
        var parameters = ParametersAddress(address);

        try
        {
            await _connection.ExecuteAsync("RegisterAddress", parameters, commandType: CommandType.StoredProcedure);
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
    public async Task ModifyAddress(Address address)
    {
        var parameters = ParametersAddress(address);

        try
        {
            await _connection.ExecuteAsync("ModifyAddress", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            throw new ConstraintException(ex.Message);
        }
    }
    public async Task DeleteAddress(int IdAddress)
    {
        await _connection.ExecuteAsync(_addressDelete, new { unIdAddress = IdAddress });
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
