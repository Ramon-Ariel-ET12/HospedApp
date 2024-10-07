using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class BedTest : AdoTest
    {
        [Fact]
        public async Task GetBeds()
        {
            var bed = await Ado.GetBeds();

            Assert.NotEmpty(bed);
        }

        [Fact]
        public async Task CreateBed()
        {
            var bed = new Bed()
            {
                Name = "Camita",
                CanSleep = 20,
            };
            await Ado.CreateBed(bed);

            var beds = await Ado.GetBeds();
            var id = beds.Max(x => x.IdBed);
            await Ado.DeleteBed(id);
        }
    }
}