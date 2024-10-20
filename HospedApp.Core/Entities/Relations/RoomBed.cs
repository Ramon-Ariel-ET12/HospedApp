namespace HospedApp.Core.Entities.Relations;

public class RoomBed
{
    public int IdRoomBed { get; set; }
    public Room? Room { get; set; }
    public Bed? Bed { get; set; }
    public int BedQuantity { get; set; }
}