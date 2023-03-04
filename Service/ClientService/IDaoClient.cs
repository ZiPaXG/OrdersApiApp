using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ClientService
{
    // DAO (data-access-object) интерфей для работы с клиентом
    public interface IDaoClient
    {
        // вариант синхронный
        //List<Client> GetAllClients();
        //Client GetById(int id);
        //Client AddClient(Client client);
        //Client UpdateClient(Client client);
        //Client DeleteClient(int id);

        Task<List<Client>> GetAllClients();
        Task<Client> GetById(int id);
        Task<Client> AddClient(Client client);
        Task<Client> UpdateClient(Client client);
        Task<bool> DeleteClient(int id);
    }
}
