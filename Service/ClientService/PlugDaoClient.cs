using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ClientService
{
    public class PlugDaoClient : IDaoClient
    {
        public static List<Client> clients = new List<Client>();

        public Task<Client> AddClient(Client client)
        {
            client.Id = clients.Count;
            clients.Add(client);
            return Task.Run(() => client); // костыль
        }

        public Task<bool> DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Client>> GetAllClients()
        {
            return Task.Run(() => clients);
        }

        public Task<Client> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
