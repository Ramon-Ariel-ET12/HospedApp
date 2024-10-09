using System.Data;
using HospedApp.Core;
using HospedApp.Core.Entities;
using HospedApp.Core.Entities.Relations;
using HospedApp.Dapper.Repositories;
using MySqlConnector;

namespace HospedApp.Dapper
{
    public class AdoDapper : IAdo
    {
        private readonly IDbConnection _conexion;
        private readonly UserDapper _userDapper;
        private readonly HotelDapper _hotelDapper;
        private readonly ClientDapper _clientDapper;
        private readonly BedDapper _bedDapper;
        private readonly RoomDapper _roomDapper;
        private readonly AddressDapper _addressDapper;
        private readonly ReservationDapper _reservationDapper;
        private readonly HotelRoomDapper _hotelroomDapper;
        private readonly RoomBedDapper _roombedDapper;

        public AdoDapper(string cadena)
        {
            _conexion = new MySqlConnection(cadena);
            _userDapper = new UserDapper(_conexion);
            _hotelDapper = new HotelDapper(_conexion);
            _clientDapper = new ClientDapper(_conexion);
            _bedDapper = new BedDapper(_conexion);
            _roomDapper = new RoomDapper(_conexion);
            _addressDapper = new AddressDapper(_conexion);
            _reservationDapper = new ReservationDapper(_conexion);
            _hotelroomDapper = new HotelRoomDapper(_conexion);
            _roombedDapper = new RoomBedDapper(_conexion);
        }

        public AdoDapper(IDbConnection conexion)
        {
            _conexion = conexion;
            _userDapper = new UserDapper(_conexion);
            _userDapper = new UserDapper(_conexion);
            _hotelDapper = new HotelDapper(_conexion);
            _clientDapper = new ClientDapper(_conexion);
            _bedDapper = new BedDapper(_conexion);
            _roomDapper = new RoomDapper(_conexion);
            _addressDapper = new AddressDapper(_conexion);
            _reservationDapper = new ReservationDapper(_conexion);
            _hotelroomDapper = new HotelRoomDapper(_conexion);
            _roombedDapper = new RoomBedDapper(_conexion);
        }


        #region 'Client'
        public List<Client> GetClients() => _clientDapper.GetClients();
        public void CreateClient(Client client) => _clientDapper.CreateClient(client);
        public void ModifyClient(Client client) => _clientDapper.ModifyClient(client);
        public void DeleteClient(int IdClient) => _clientDapper.DeleteClient(IdClient);
        #endregion


        #region 'User'
        public User? Login(string email, string password) => _userDapper.Login(email, password);
        #endregion


        #region 'Bed'
        public List<Bed> GetBeds() => _bedDapper.GetBeds();
        public void CreateBed(Bed bed) => _bedDapper.CreateBed(bed);
        public void ModifyBed(Bed bed) => _bedDapper.ModifyBed(bed);
        public void DeleteBed(int IdBed) => _bedDapper.DeleteBed(IdBed);
        #endregion


        #region 'Room'
        public List<Room> GetRooms() => _roomDapper.GetRooms();
        public void CreateRoom(Room room) => _roomDapper.CreateRoom(room);
        public void ModifyRoom(Room room) => _roomDapper.ModifyRoom(room);
        public void DeleteRoom(int IdRoom) => _roomDapper.DeleteRoom(IdRoom);
        #endregion


        #region 'Hotel'
        public List<Hotel> GetHotels() => _hotelDapper.GetHotels();
        public void CreateHotel(Hotel hotel) => _hotelDapper.CreateHotel(hotel);
        public void ModifyHotel(Hotel hotel) => _hotelDapper.ModifyHotel(hotel);
        public void DeleteHotel(int IdHotel) => _hotelDapper.DeleteHotel(IdHotel);
        #endregion


        #region 'RoomBed'
        public List<RoomBed> GetRoomBeds() => _roombedDapper.GetRoomBeds();
        public void CreateRoomBed(RoomBed roomBed) => _roombedDapper.CreateRoomBed(roomBed);
        public void DeleteRoomBed(int IdRoom, int IdBed) => _roombedDapper.DeleteRoomBed(IdRoom, IdBed);
        #endregion


        #region  'Address'

        public List<Address> GetAddresses() => _addressDapper.GetAddresses();
        public void CreateAddress(Address address) => _addressDapper.CreateAddress(address);
        public void ModifyAddress(Address address) => _addressDapper.ModifyAddress(address);
        public void DeleteAddress(int IdAddress) => _addressDapper.DeleteAddress(IdAddress);
        #endregion


        #region 'HotelRoom'
        public List<HotelRoom> GetHotelRooms() => _hotelroomDapper.GetHotelRooms();
        public void CreateHotelRoom(HotelRoom hotelRoom) => _hotelroomDapper.CreateHotelRoom(hotelRoom);
        public void DeleteHotelRoom(int IdHotel, int RoomNumber) => _hotelroomDapper.DeleteHotelRoom(IdHotel, RoomNumber);
        #endregion


        #region 'Reservation'
        public List<Reservation> GetReservations() => _reservationDapper.GetReservations();
        public List<Reservation> GetReservationsCancelled() => _reservationDapper.GetReservationsCancelled();
        public void CreateReservation(Reservation reservation) => _reservationDapper.CreateReservation(reservation);
        public void ModifyReservation(Reservation reservation) => _reservationDapper.ModifyReservation(reservation);
        public void CancelReservation(int IdReservation) => _reservationDapper.CancelReservation(IdReservation);
        #endregion
    }
}
