using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class BedTest : AdoTest
    {
        [Fact]
        public void GetBeds()
        {
            var bed = Ado.GetBeds();

            Assert.NotEmpty(bed);
        }

        [Fact]
        public void CreateBed()
        {
            var bed = new Bed()
            {
                Name = "Camita",
                CanSleep = 20,
            };
            Ado.CreateBed(bed);

            var beds = Ado.GetBeds();
            var id = beds.Max(x => x.IdBed);
            Ado.DeleteBed(id);
        }
    }
}