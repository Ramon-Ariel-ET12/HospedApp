using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories;

public class ClientDapper
{
    private readonly IDbConnection _conexion;

    public ClientDapper(IDbConnection conexion) => _conexion = conexion;

    #region 'Client'
    private readonly string _ClientQuery
        = @"SELECT * FROM Client";
    private readonly string _ClientDelete
        = @"DELETE FROM Client WHERE IdClient = @unIdClient";

    public async Task<List<Client>> GetClients()
    {
        var client = (await _conexion.QueryAsync<Client>(_ClientQuery)).ToList();
        return client;
    }

    public async Task CreateClient(Client client)
    {
        var parameters = ParametersClient(client);

        try
        {
            await _conexion.ExecuteAsync("RegisterClient", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            if (ex.Number == 1644)
            {
                if (ex.Message.Contains("dni"))
                    throw new ConstraintException($"El dni ingresado {client.Dni} es erronea, debe ser de 8 digitos!");
            }
            if (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (ex.Message.Contains("Dni"))
                    throw new ConstraintException($"El Dni {client.Dni} ya existe");
                if (ex.Message.Contains("Name"))
                    throw new ConstraintException($"El nombre {client.Name} ya existe");
                if (ex.Message.Contains("Phone"))
                    throw new ConstraintException($"El telefono {client.Phone} ya existe");
                if (ex.Message.Contains("Email"))
                    throw new ConstraintException($"El correo {client.Email} ya existe");
            }
            throw;
        }
    }
    public async Task DeleteClient(int IdClient)
    {
        await _conexion.ExecuteAsync(_ClientDelete, new { unIdClient = IdClient });
    }
    public static DynamicParameters ParametersClient(Client client)
    {
        var parameters = new DynamicParameters();

        if (client.IdClient == 0)
            parameters.Add("@unIdClient", client.IdClient);

        parameters.Add("@unDni", client.Dni);
        parameters.Add("@unName", client.Name);
        parameters.Add("@unLastName", client.LastName);
        parameters.Add("@unSex", client.Sex);
        parameters.Add("@unPhone", client.Phone);
        parameters.Add("@unEmail", client.Email);
        parameters.Add("@unPass", client.Pass);

        return parameters;
    }
    
}

    #endregion