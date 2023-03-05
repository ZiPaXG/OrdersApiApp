using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ClientService
{
    // DAO (data-access-object) интерфейс для работы с клиентом
    public interface IDaoClient
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetById(int id);
        Task<Client> AddClient(Client client);
        Task<Client> UpdateClient(Client client);
        Task<bool> DeleteClient(int id);
    }
}
