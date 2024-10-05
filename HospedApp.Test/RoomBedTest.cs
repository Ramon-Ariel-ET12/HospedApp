using System.Xml.Linq;
using HospedApp.Core.Entities.Relations;

namespace HospedApp.Test
{
    public class RoomBedTest : AdoTest
    {
        [Fact]
        public void GetRoomBeds()
        {
            var roomBed = Ado.GetRoomBeds();

            Assert.NotEmpty(roomBed);
        }

        [Fact]
        public void CreateRoomBed()
        {
            var roombed = new RoomBed()
            {
                Room = new(){ IdRoom = 20},
                Bed = new(){ IdBed = 3},
                BedQuantity = 20,
            };

            Ado.CreateRoomBed(roombed);

            Ado.DeleteRoomBed(roombed.Room.IdRoom, roombed.Bed.IdBed);
        }
    }
}