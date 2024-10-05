namespace HospedApp.Core.Entities;

public class User
{
    public int IdUser { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Pass { get; set; }
}