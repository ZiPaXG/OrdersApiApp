using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderService
{
    public class DbdDaoOrder : IDaoTemplate<Order>
    {
        private ApplicationDbContext context;

        public DbdDaoOrder(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Order> Add(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAll()
        {
            await context.Clients.LoadAsync();
            return await context.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            Order? order = await context.Orders.FirstOrDefaultAsync(t => t.Id == id);
            return order;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await context.OrderProducts.LoadAsync();
                IEnumerable<OrderProduct> result = await context.OrderProducts.Where(t => t.OrderId == id).ToListAsync();
                if (result.Count() != 0)
                    context.OrderProducts.RemoveRange(result);

                Order? order = await context.Orders.FirstOrDefaultAsync(t => t.Id == id);
                if (order != null )
                {
                    context.Orders.Remove(order);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception();
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Order> Update(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return order;
        }

    }
}
