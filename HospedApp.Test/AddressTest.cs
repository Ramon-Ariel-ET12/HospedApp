using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class AddressTest : AdoTest
    {
        [Fact]
        public void GetAddresses()
        {
            var address = Ado.GetAddresses();

            Assert.NotEmpty(address);
        }

        [Fact]
        public void CreateAddress()
        {
            var address = new Address()
            {
                Hotel = new() { IdHotel = 1},
                Domicile = "Hola 123",
                PostalCode = 1104,
            };

            Ado.CreateAddress(address);

            var addresses = Ado.GetAddresses();
            var id = addresses.Max(x => x.IdAddress);

            Ado.DeleteAddress(id);
        }
    }
}