using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderService
{
    public class DbdDaoOrder : IDaoOrder
    {
        private ApplicationDbContext context;

        public DbdDaoOrder(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Order> AddOrder(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            Order order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
            return order != null;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            await context.Clients.LoadAsync();
            return await context.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            Order? order = await context.Orders.FirstOrDefaultAsync(t => t.Id == id);
            return order;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return order;
        }
    }
}
