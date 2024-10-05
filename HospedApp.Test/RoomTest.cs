using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class RoomTest : AdoTest
    {
        [Fact]
        public void GetRooms()
        {
            var room = Ado.GetRooms();

            Assert.NotEmpty(room);
        }

        [Fact]
        public void CreateRoom()
        {
            var room = new Room()
            {
                Garage = "CON",
                PriceNight = 20_000.00,
                Description = "Comoda",
            };

            Ado.CreateRoom(room);

            var rooms = Ado.GetRooms();
            var id = rooms.Max(x => x.IdRoom);
            Ado.DeleteRoom(id);
        }
    }
}