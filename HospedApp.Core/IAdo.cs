﻿using HospedApp.Core.Entities;
using HospedApp.Core.Entities.Relations;

namespace HospedApp.Core;

public interface IAdo
{
    #region 'Client'
    Task<List<Client>> GetClients();
    Task CreateClient(Client client);
    Task ModifyClient(Client client);
    Task DeleteClient(int IdClient);
    #endregion


    #region 'User'
    Task<User?> Login(string email, string password);
    #endregion


    #region 'Bed'
    Task<List<Bed>> GetBeds();
    Task CreateBed(Bed bed);
    Task ModifyBed(Bed client);
    Task DeleteBed(int IdBed);
    #endregion


    #region 'Room'
    Task<List<Room>> GetRooms();
    Task CreateRoom(Room room);
    Task ModifyRoom(Room client);
    Task DeleteRoom(int IdRoom);
    #endregion

    #region 'Hotel'
    Task<List<Hotel>> GetHotels();
    Task CreateHotel(Hotel hotel);
    Task ModifyHotel(Hotel client);
    Task DeleteHotel(int IdHotel);
    #endregion


    #region 'RoomBed'
    Task<List<RoomBed>> GetRoomBeds();
    Task CreateRoomBed(RoomBed roomBed);
    Task ModifyRoomBed(RoomBed roomBed);
    Task DeleteRoomBed(int IdRoom, int IdBed);
    #endregion


    #region 'Address'
    Task<List<Address>> GetAddresses();
    Task CreateAddress(Address address);
    Task ModifyAddress(Address client);
    Task DeleteAddress(int IdAddress);
    #endregion


    #region 'HotelRoom'
    Task<List<HotelRoom>> GetHotelRooms();
    Task CreateHotelRoom(HotelRoom hotelRoom);
    Task DeleteHotelRoom(int IdHotel, int RoomNumber);
    #endregion


    #region 'Reservation'
    Task<List<Reservation>> GetReservations();
    Task<List<Reservation>> GetReservationsCancelled();
    Task CreateReservation(Reservation reservation);
    Task ModifyReservation(Reservation client);
    Task CancelReservation(int IdReservation);
    #endregion

}
