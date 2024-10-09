using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class HotelTest : AdoTest
    {
        [Fact]
        public async Task GetHotels()
        {
            var hotels = await Ado.GetHotels();

            Assert.NotEmpty(hotels);
        }

        [Fact]
        public async Task CreateHotel()
        {
            var hotel = new Hotel()
            {
                Name = "Prueba",
                Email = "Prueba",
                Web = "Prueba",
                Star = 100,
            };

            await Ado.CreateHotel(hotel);

            var hotels = await Ado.GetHotels();
            int id = hotels.Max(x => x.IdHotel);
            await Ado.DeleteHotel(id);
        }
    }
}