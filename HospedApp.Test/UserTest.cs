namespace HospedApp.Test
{
    public class UserTest : AdoTest
    {
        [Fact]
        public void Login()
        {
            var user = Ado.Login("eljonsu@gmail.com", "JonAlv");

            Assert.NotNull(user);
        }
    }
}