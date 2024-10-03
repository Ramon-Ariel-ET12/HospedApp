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

    private readonly string _queryHotel
        = @"SELECT * FROM Hotel";
    public List<Hotel> GetHotel()
        => _conexion.Query<Hotel>(_queryHotel).ToList();

    public void CreateHotel(Hotel hotel)
    {
        throw new NotImplementedException();
    }


    #endregion
}
