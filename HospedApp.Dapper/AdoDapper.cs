﻿using System.Data;
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
        public async Task<List<Client>> GetClients() => await _clientDapper.GetClients();
        public async Task CreateClient(Client client) => await _clientDapper.CreateClient(client);
        public async Task ModifyClient(Client client) => await _clientDapper.ModifyClient(client);
        public async Task DeleteClient(int IdClient) => await _clientDapper.DeleteClient(IdClient);
        #endregion


        #region 'User'
        public async Task<User?> Login(string email, string password) => await _userDapper.Login(email, password);
        #endregion


        #region 'Bed'
        public async Task<List<Bed>> GetBeds() => await _bedDapper.GetBeds();
        public async Task CreateBed(Bed bed) => await _bedDapper.CreateBed(bed);
        public async Task ModifyBed(Bed bed) => await _bedDapper.ModifyBed(bed);
        public async Task DeleteBed(int IdBed) => await _bedDapper.DeleteBed(IdBed);
        #endregion


        #region 'Room'
        public async Task<List<Room>> GetRooms() => await _roomDapper.GetRooms();
        public async Task CreateRoom(Room room) => await _roomDapper.CreateRoom(room);
        public async Task ModifyRoom(Room room) => await _roomDapper.ModifyRoom(room);
        public async Task DeleteRoom(int IdRoom) => await _roomDapper.DeleteRoom(IdRoom);
        #endregion


        #region 'Hotel'
        public async Task<List<Hotel>> GetHotels() => await _hotelDapper.GetHotels();
        public async Task CreateHotel(Hotel hotel) => await _hotelDapper.CreateHotel(hotel);
        public async Task ModifyHotel(Hotel hotel) => await _hotelDapper.ModifyHotel(hotel);
        public async Task DeleteHotel(int IdHotel) => await _hotelDapper.DeleteHotel(IdHotel);
        #endregion


        #region 'RoomBed'
        public async Task<List<RoomBed>> GetRoomBeds() => await _roombedDapper.GetRoomBeds();
        public async Task CreateRoomBed(RoomBed roomBed) => await _roombedDapper.CreateRoomBed(roomBed);
        public async Task ModifyRoomBed(RoomBed roomBed) => await _roombedDapper.ModifyRoomBed(roomBed);
        public async Task DeleteRoomBed(int IdRoom, int IdBed) => await _roombedDapper.DeleteRoomBed(IdRoom, IdBed);
        #endregion


        #region  'Address'

        public async Task<List<Address>> GetAddresses() => await _addressDapper.GetAddresses();
        public async Task CreateAddress(Address address) => await _addressDapper.CreateAddress(address);
        public async Task ModifyAddress(Address address) => await _addressDapper.ModifyAddress(address);
        public async Task DeleteAddress(int IdAddress) => await _addressDapper.DeleteAddress(IdAddress);
        #endregion


        #region 'HotelRoom'
        public async Task<List<HotelRoom>> GetHotelRooms() => await _hotelroomDapper.GetHotelRooms();
        public async Task CreateHotelRoom(HotelRoom hotelRoom) => await _hotelroomDapper.CreateHotelRoom(hotelRoom);
        public async Task DeleteHotelRoom(int IdAddress, int RoomNumber) => await _hotelroomDapper.DeleteHotelRoom(IdAddress, RoomNumber);
        #endregion


        #region 'Reservation'
        public async Task<List<Reservation>> GetReservations() => await _reservationDapper.GetReservations();
        public async Task<List<Reservation>> GetReservationsCancelled() => await _reservationDapper.GetReservationsCancelled();
        public async Task CreateReservation(Reservation reservation) => await _reservationDapper.CreateReservation(reservation);
        public async Task ModifyReservation(Reservation reservation) => await _reservationDapper.ModifyReservation(reservation);
        public async Task CancelReservation(int IdReservation) => await _reservationDapper.CancelReservation(IdReservation);
        #endregion
    }
}