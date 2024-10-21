using HospedApp.Core.Entities.Relations;

namespace HospedApp.Core.Entities;

public class Reservation
{
    public int IdReservation { get; set; }
    public Client? Client { get; set; }
    public Address? Address { get; set; }
    public RoomBed? RoomBed { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get ; set ; }
    public string? ClientComment { get; set; }
    public bool Active { get; set; }
}