namespace HospedApp.Core.Entities;

public class Reservation
{
    public int IdReservation { get; set; }
    public Client? Client { get; set; }
    public Hotel? Hotel { get; set; }
    public Address? Address { get; set; }
    public Room? Room { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get ; set ; }
    public string? ClientComment { get; set; }
    public bool Active { get; set; }
}