using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ClientService
{
    public class DbdDaoClient : IDaoClient
    {
        public ApplicationDbContext context;
        public DbdDaoClient(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Client> AddClient(Client client)
        {
            client.Id = await context.Clients.CountAsync() + 1;
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            return await Task.Run(() => client);
        }

        public async Task<bool> DeleteClient(int id)
        {
            Client? client = await context.Clients.FirstOrDefaultAsync(t => t.Id == id);
            context.Clients.Remove(client);
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await Task.Run(() => context.Clients.ToListAsync());
        }

        public async Task<Client> GetById(int id)
        {
            Client? client = await context.Clients.FirstOrDefaultAsync(t => t.Id == id);
            return await Task.Run(() => client);
        }

        public async Task<Client> UpdateClient(Client client)
        {
            context.Clients.Update(client);
            await context.SaveChangesAsync();
            return await Task.Run(() => client);
        }
    }
}
