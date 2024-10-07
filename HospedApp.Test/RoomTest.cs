using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class RoomTest : AdoTest
    {
        [Fact]
        public async Task GetRooms()
        {
            var room = await Ado.GetRooms();

            Assert.NotEmpty(room);
        }

        [Fact]
        public async Task CreateRoom()
        {
            var room = new Room()
            {
                Garage = "CON",
                PriceNight = 20_000.00,
                Description = "Comoda",
            };

            await Ado.CreateRoom(room);

            var rooms = await Ado.GetRooms();
            var id = rooms.Max(x => x.IdRoom);
            await Ado.DeleteRoom(id);
        }
    }
}