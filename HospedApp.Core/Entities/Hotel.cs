namespace HospedApp.Core.Entities;

public class Hotel
{
    public int IdHotel { get; set; }
    public string? Name { get; set; }
    public List<Address>? Addresses { get; set; }
    public string? Email { get; set; }
    public string? Web { get; set; }
    public byte Star { get; set; }
}