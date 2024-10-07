namespace HospedApp.Test
{
    public class UserTest : AdoTest
    {
        [Fact]
        public async Task Login()
        {
            var user = await Ado.Login("eljonsu@gmail.com", "JonAlv");

            Assert.NotNull(user);
        }
    }
}