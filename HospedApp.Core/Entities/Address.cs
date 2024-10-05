namespace HospedApp.Core.Entities;

public class Address
{
    public int IdAddress { get; set; }
    public Hotel? Hotel { get; set; }
    public string? Domicile { get; set; }
    public int PostalCode { get; set; }
}