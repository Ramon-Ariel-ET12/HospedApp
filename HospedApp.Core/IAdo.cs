using HospedApp.Core.Entities;

namespace HospedApp.Core;

public interface IAdo
{
    #region 'Hotel'

    List<Hotel> GetHotel();

    void CreateHotel(Hotel hotel);
    #endregion
}
