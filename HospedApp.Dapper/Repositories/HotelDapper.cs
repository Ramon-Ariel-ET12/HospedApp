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

        public List<Hotel> GetHotels()
        {
            var hotelDictionary = new Dictionary<int, Hotel>();

            var hotels = _conexion.Query<Hotel, Address, Hotel>(_HotelQuery,
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
            ).Distinct().ToList();

            return hotels;
        }

        public void CreateHotel(Hotel hotel)
        {
            var parameters = ParametersHotel(hotel);
            try
            {
                _conexion.Execute("RegisterHotel", parameters, commandType: CommandType.StoredProcedure);
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
        public void ModifyHotel(Hotel hotel)
        {
            var parameters = ParametersHotel(hotel);
            try
            {
                _conexion.Execute("ModifyHotel", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (MySqlException ex)
            {
                throw new ConstraintException(ex.Message);
            }
        }

        public void DeleteHotel(int IdHotel)
        {
            _conexion.Execute(_HotelDelete, new { unIdHotel = IdHotel });
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
