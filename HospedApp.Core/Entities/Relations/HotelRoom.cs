namespace HospedApp.Core.Entities.Relations;

public class HotelRoom
{
    public Hotel? Hotel { get; set; }
    public Room? Room { get; set; }
    public byte Number { get; set; }
}