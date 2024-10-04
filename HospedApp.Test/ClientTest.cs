using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class ClientTest : AdoTest
    {
        [Fact]
        public void GetClients()
        {
            var client = Ado.GetClients();
            Assert.NotEmpty(client);
        }
        [Fact]
        public void CreateClients()
        {
            var client = new Client()
            {
                Dni = 19287346,
                Name = "Roque",
                LastName = "Rivas",
                Phone = "912837",
                Email = "nose",
                Pass = "alñskjd",
            };
            Ado.CreateClient(client);

            var clients = Ado.GetClients();
            int? id = clients.Max(x => x.IdClient);
            Ado.DeleteClient(id);
        }
    }
}