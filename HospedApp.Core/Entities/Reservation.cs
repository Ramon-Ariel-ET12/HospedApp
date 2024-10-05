namespace HospedApp.Core.Entities;

public class Reservation
{
    public int IdReservation { get; set; }
    public Client? Client { get; set; }
    public Hotel? Hotel { get; set; }
    public Room? Room { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get ; set ; }
    public string? ClientComment { get; set; }
    public bool Active { get; set; }
}