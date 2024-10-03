namespace HospedApp.Test
{
    public class HotelTest : AdoTest
    {
        [Fact]
        public void GetHotel()
        {
            var hotels = Ado.GetHotel();

            Assert.NotEmpty(hotels);
        }
    }
}