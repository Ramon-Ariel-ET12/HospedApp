using HospedApp.Core.Entities.Relations;

namespace HospedApp.Test
{
    public class HotelRoomTest : AdoTest
    {
        [Fact]
        public void GetHotelRooms()
        {
            var room = Ado.GetHotelRooms();

            Assert.NotEmpty(room);
        }

        [Fact]
        public void CreateHotelRoom()
        {
            var hotelroom = new HotelRoom()
            {
                Hotel = new (){ IdHotel = 4},
                Address = new(){ IdAddress = 13},
                Room = new(){ IdRoom = 10},
            };

            Ado.CreateHotelRoom(hotelroom);

            var hotelrooms = Ado.GetHotelRooms();

            var number = hotelrooms.Where(x => x.Hotel!.IdHotel == hotelroom.Hotel.IdHotel).Max(x => x.Number);

            Ado.DeleteHotelRoom(hotelroom.Hotel.IdHotel, number);
        }
    }
}