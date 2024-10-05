using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class HotelTest : AdoTest
    {
        [Fact]
        public void GetHotels()
        {
            var hotels = Ado.GetHotels();

            Assert.NotEmpty(hotels);
        }

        [Fact]
        public void CreateHotel()
        {
            var hotel = new Hotel()
            {
                Name = "Prueba",
                Phone = "Prueba",
                Email = "Prueba",
                Web = "Prueba",
                Star = 100,
            };

            Ado.CreateHotel(hotel);

            var hotels = Ado.GetHotels();
            int id = hotels.Max(x => x.IdHotel);
            Ado.DeleteHotel(id);
        }
    }
}