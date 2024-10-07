using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class AddressTest : AdoTest
    {
        [Fact]
        public async Task GetAddresses()
        {
            var address = await Ado.GetAddresses();

            Assert.NotEmpty(address);
        }

        [Fact]
        public async Task CreateAddress()
        {
            var address = new Address()
            {
                Hotel = new() { IdHotel = 1},
                Domicile = "Hola 123",
                PostalCode = 1104,
            };

            await Ado.CreateAddress(address);

            var addresses = await Ado.GetAddresses();
            var id = addresses.Max(x => x.IdAddress);

            await Ado.DeleteAddress(id);
        }
    }
}