using System.Xml.Linq;
using HospedApp.Core.Entities.Relations;

namespace HospedApp.Test
{
    public class RoomBedTest : AdoTest
    {
        [Fact]
        public async Task GetRoomBeds()
        {
            var roomBed = await Ado.GetRoomBeds();

            Assert.NotEmpty(roomBed);
        }

        [Fact]
        public async Task CreateRoomBed()
        {
            var roombed = new RoomBed()
            {
                Room = new(){ IdRoom = 20},
                Bed = new(){ IdBed = 3},
                BedQuantity = 20,
            };

            await Ado.CreateRoomBed(roombed);

            await Ado.DeleteRoomBed(roombed.Room.IdRoom, roombed.Bed.IdBed);
        }
    }
}