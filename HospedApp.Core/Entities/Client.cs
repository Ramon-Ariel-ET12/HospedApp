namespace HospedApp.Core.Entities;

public class Client
{
    public int? IdClient { get; set; }
    public int? Dni { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Sex { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Pass { get; set; }
}