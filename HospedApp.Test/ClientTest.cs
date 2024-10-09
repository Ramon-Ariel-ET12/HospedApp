using HospedApp.Core.Entities;

namespace HospedApp.Test
{
    public class ClientTest : AdoTest
    {
        [Fact]
        public async Task GetClients()
        {
            var client = await Ado.GetClients();
            Assert.NotEmpty(client);
        }
        [Fact]
        public async Task CreateClients()
        {
            var client = new Client()
            {
                Dni = 19287346,
                Name = "Roque",
                LastName = "Rivas",
                Sex = "M",
                Email = "nose",
                Pass = "alñskjd",
            };
            await Ado.CreateClient(client);

            var clients = await Ado.GetClients();
            int id = clients.Max(x => x.IdClient);
            await Ado.DeleteClient(id);
        }
    }
}