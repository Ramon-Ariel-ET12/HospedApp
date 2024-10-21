namespace HospedApp.Core.Entities.Relations;

public class HotelRoom
{
    public Address? Address { get; set; }
    public RoomBed? RoomBed { get; set; }
    public byte Number { get; set; }
}