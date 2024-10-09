using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories
{
    public class HotelDapper
    {
        private readonly IDbConnection _conexion;

        public HotelDapper(IDbConnection conexion) => _conexion = conexion;

        private readonly string _HotelQuery
            = @"SELECT h.*, a.* FROM Hotel h
                LEFT JOIN Address a ON h.IdHotel = a.IdHotel";
        private readonly string _HotelDelete
            = @"DELETE FROM Hotel WHERE IdHotel = @unIdHotel";

        public async Task<List<Hotel>> GetHotels()
        {
            var hotelDictionary = new Dictionary<int, Hotel>();

            var hotels = (await _conexion.QueryAsync<Hotel, Address, Hotel>(_HotelQuery,
                (hotel, address) =>
                {
                    if (!hotelDictionary.TryGetValue(hotel.IdHotel, out var currentHotel))
                    {
                        currentHotel = hotel;
                        currentHotel.Addresses = new List<Address>();
                        hotelDictionary.Add(hotel.IdHotel, currentHotel);
                    }
                    if (address != null)
                    {
                        currentHotel.Addresses!.Add(address);
                    }
                    return currentHotel;
                },
                splitOn: "IdAddress"
            )).Distinct().ToList();

            return hotels;
        }

        public async Task CreateHotel(Hotel hotel)
        {
            var parameters = ParametersHotel(hotel);
            try
            {
                await _conexion.ExecuteAsync("RegisterHotel", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (MySqlException ex)
            {
                if (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    if (ex.Message.Contains("Name"))
                        throw new ConstraintException($"El nombre {hotel.Name} ya existe");
                    if (ex.Message.Contains("Email"))
                        throw new ConstraintException($"El correo {hotel.Email} ya existe");
                }
                throw;
            }
        }
        public async Task ModifyHotel(Hotel hotel)
        {
            var parameters = ParametersHotel(hotel);
            try
            {
                await _conexion.ExecuteAsync("ModifyHotel", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (MySqlException ex)
            {
                throw new ConstraintException(ex.Message);
            }
        }

        public async Task DeleteHotel(int IdHotel)
        {
            await _conexion.ExecuteAsync(_HotelDelete, new { unIdHotel = IdHotel });
        }

        private static DynamicParameters ParametersHotel(Hotel hotel)
        {
            var parameters = new DynamicParameters();
            if (hotel.IdHotel != 0)
                parameters.Add("@unIdHotel", hotel.IdHotel);
            parameters.Add("@unName", hotel.Name);
            parameters.Add("@unEmail", hotel.Email);
            parameters.Add("@unWeb", hotel.Web);
            parameters.Add("@unStar", hotel.Star);

            return parameters;
        }
    }
}
