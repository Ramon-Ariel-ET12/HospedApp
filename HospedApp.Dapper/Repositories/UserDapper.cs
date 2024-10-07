using System.Data;
using Dapper;
using HospedApp.Core.Entities;

namespace HospedApp.Dapper.Repositories;

public class UserDapper
{
    private IDbConnection _conexion;
    public UserDapper(IDbConnection connection) => _conexion = connection;

    private readonly string _UserQueryEmailPass
        = @"CALL GetEmailPass(@unEmail, @unPass)";

    public async Task<User?> Login(string Email, string Pass)
    {
        var user = await _conexion.QueryFirstOrDefaultAsync<User>(_UserQueryEmailPass, new { unEmail = Email, unPass = Pass });
        return user;
    }
}