using HospedApp.Core.Entities;
using HospedApp.Core.Entities.Relations;

namespace HospedApp.Core;

public interface IAdo
{
    #region 'Client'
    List<Client> GetClients();
    void CreateClient(Client client);
    void DeleteClient(int IdClient);
    #endregion


    #region 'User'
    User? Login(string email, string password);
    #endregion


    #region 'Bed'
    List<Bed> GetBeds();
    void CreateBed(Bed bed);
    void DeleteBed(int IdBed);
    #endregion


    #region 'Room'
    List<Room> GetRooms();
    void CreateRoom(Room room);
    void DeleteRoom(int IdRoom);
    #endregion

    #region 'Hotel'
    List<Hotel> GetHotels();
    void CreateHotel(Hotel hotel);
    void DeleteHotel(int IdHotel);
    #endregion


    #region 'RoomBed'
    List<RoomBed> GetRoomBeds();
    void CreateRoomBed(RoomBed roomBed);
    void DeleteRoomBed(int IdRoom, int IdBed);
    #endregion


    #region 'Address'
    List<Address> GetAddresses();
    void CreateAddress(Address address);
    void DeleteAddress(int IdAddress);
    #endregion


    #region 'HotelRoom'
    List<HotelRoom> GetHotelRooms();
    void CreateHotelRoom(HotelRoom hotelRoom);
    void DeleteHotelRoom(int IdHotel, int RoomNumber);
    #endregion


    #region 'Reservation'
    List<Reservation> GetReservations();
    List<Reservation> GetReservationsCancelled();
    void CreateReservation(Reservation reservation);
    void CancelReservation(int IdReservation);
    #endregion

}
