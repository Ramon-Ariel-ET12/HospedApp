using HospedApp.Core.Entities.Relations;

namespace HospedApp.Test
{
    public class HotelRoomTest : AdoTest
    {
        [Fact]
        public async Task GetHotelRooms()
        {
            var room = await Ado.GetHotelRooms();

            Assert.NotEmpty(room);
        }

        [Fact]
        public async Task CreateHotelRoom()
        {
            var hotelroom = new HotelRoom()
            {
                Hotel = new (){ IdHotel = 4},
                Address = new(){ IdAddress = 13},
                Room = new(){ IdRoom = 10},
            };

            await Ado.CreateHotelRoom(hotelroom);

            var hotelrooms = await Ado.GetHotelRooms();

            var number = hotelrooms.Where(x => x.Hotel!.IdHotel == hotelroom.Hotel.IdHotel).Max(x => x.Number);

            await Ado.DeleteHotelRoom(hotelroom.Hotel.IdHotel, number);
        }
    }
}