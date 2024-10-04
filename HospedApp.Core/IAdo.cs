using HospedApp.Core.Entities;

namespace HospedApp.Core;

public interface IAdo
{

    #region 'Hotel'
    List<Hotel> GetHotels();
    void CreateHotel(Hotel hotel);
    void DeleteHotel(int? IdHotel);
    #endregion
    /*********************************************/
    #region 'Client'
    List<Client> GetClients();
    void CreateClient(Client client);
    void DeleteClient(int? IdClient);
    #endregion
}
