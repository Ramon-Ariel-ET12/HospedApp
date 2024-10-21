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
                Address = new(){ IdAddress = 13},
                RoomBed = new(){ IdRoomBed = 10},
            };

            await Ado.CreateHotelRoom(hotelroom);

            var hotelrooms = await Ado.GetHotelRooms();

            var number = hotelrooms.Where(x => x.Address!.IdAddress == hotelroom!.Address!.IdAddress).Max(x => x.Number);

            await Ado.DeleteHotelRoom(hotelroom.Address.IdAddress, number);
        }
    }
}