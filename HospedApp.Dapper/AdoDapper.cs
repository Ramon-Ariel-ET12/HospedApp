using System.Data;
using Dapper;
using HospedApp.Core;
using HospedApp.Core.Entities;
using MySqlConnector;
namespace HospedApp.Dapper;

public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;
    //Este constructor usa por defecto la cadena para un conector MySQL
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);
    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;

    #region 'Hotel'
    private readonly string _HotelQuery
        = @"SELECT * FROM Hotel";
    private readonly string _HotelDelete
        = @"DELETE FROM Hotel WHERE IdHotel = @unIdHotel";

    /***************************************************************************************/
    public List<Hotel> GetHotels()
    {
        var hotel = _conexion.Query<Hotel>(_HotelQuery).ToList();
        return hotel;
    }
    /***************************************************************************************/
    public void CreateHotel(Hotel hotel)
    {
        var parameters = ParametersHotel(hotel);
        try
        {
            _conexion.Execute("RegisterHotel", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (error.Message.Contains("Name"))
                    throw new ConstraintException($"El nombre {hotel.Name} ya existe");
                if (error.Message.Contains("Phone"))
                    throw new ConstraintException($"El telefono {hotel.Phone} ya existe");
                if (error.Message.Contains("Email"))
                    throw new ConstraintException($"El correo {hotel.Email} ya existe");
            }
            throw;
        }
    }
    /***************************************************************************************/
    public void DeleteHotel(int? IdHotel)
    {
        _conexion.Query(_HotelDelete, new { unIdHotel = IdHotel });
    }
    /***************************************************************************************/
    private static DynamicParameters ParametersHotel(Hotel hotel)
    {
        var parameters = new DynamicParameters();

        if (hotel.IdHotel != 0 || hotel.IdHotel != null)
            parameters.Add("@unIdHotel", hotel.IdHotel);
        parameters.Add("@unName", hotel.Name);
        parameters.Add("@unPhone", hotel.Phone);
        parameters.Add("@unEmail", hotel.Email);
        parameters.Add("@unWeb", hotel.Web);
        parameters.Add("@unStar", hotel.Star);

        return parameters;
    }
    /***************************************************************************************/
    #endregion

    #region 'Client'
    private readonly string _ClientQuery
        = @"SELECT * FROM Client";
    private readonly string _ClientDelete
        = @"DELETE FROM Client WHERE IdClient = @unIdClient";
    /***************************************************************************************/

    public List<Client> GetClients()
    {
        var client = _conexion.Query<Client>(_ClientQuery).ToList();
        return client;
    }
    /***************************************************************************************/

    public void CreateClient(Client client)
    {
        var parameters = ParametersClient(client);

        try
        {
            _conexion.Execute("RegisterClient", parameters, commandType: CommandType.StoredProcedure);
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
    /***************************************************************************************/
    public void DeleteClient(int? IdClient)
    {
        _conexion.Query(_ClientDelete, new { unIdClient = IdClient });
    }
    /***************************************************************************************/

    public static DynamicParameters ParametersClient(Client client)
    {
        var parameters = new DynamicParameters();

        if (client.IdClient == 0 || client.IdClient == null)
            parameters.Add("@unIdClient", client.IdClient);

        parameters.Add("@unDni", client.Dni);
        parameters.Add("@unName", client.Name);
        parameters.Add("@unLastName", client.LastName);
        parameters.Add("@unPhone", client.Phone);
        parameters.Add("@unEmail", client.Email);
        parameters.Add("@unPass", client.Pass);

        return parameters;
    }
    /***************************************************************************************/

    #endregion
}
