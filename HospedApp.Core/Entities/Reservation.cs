namespace HospedApp.Core.Entities;

public class Reservation
{
    public int IdReservation { get; set; }
    public Client? Client { get; set; }
    public Hotel? Hotel { get; set; }
    public Room? Room { get; set; }
    public DateOnly? StartDate { get => StartDate; set => value?.ToString("YYYY-MM-DD"); }
    public DateOnly? EndDate { get => EndDate; set => value?.ToString("YYYY-MM-DD"); }
    public string? ClientComment { get; set; }
    public bool? Active { get; set; }
}