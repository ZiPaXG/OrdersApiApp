using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderProductService
{
    public class DbdDaoOrderProduct : IDaoTemplate<OrderProduct>
    {
        public ApplicationDbContext context;
        public DbdDaoOrderProduct(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<OrderProduct> Add(OrderProduct orderProduct)
        {
            await context.OrderProducts.AddAsync(orderProduct);
            await context.SaveChangesAsync();
            return orderProduct;
        }

        public async Task<bool> Delete(int id)
        {
            OrderProduct? orderProduct = await context.OrderProducts.FirstOrDefaultAsync(t => t.Id == id);
            if (orderProduct != null)
            {
                context.OrderProducts.Remove(orderProduct);
                await context.SaveChangesAsync();
            }
            return orderProduct != null;
        }

        public async Task<List<OrderProduct>> GetAll()
        {
            await context.OrderProducts.LoadAsync();
            return await context.OrderProducts.ToListAsync();
        }

        public async Task<OrderProduct> GetById(int id)
        {
            OrderProduct? orderProduct = await context.OrderProducts.FirstOrDefaultAsync(x => x.Id == id);
            return orderProduct;
        }

        public async Task<OrderProduct> Update(OrderProduct orderProduct)
        {
            context.OrderProducts.Update(orderProduct);
            await context.SaveChangesAsync();
            return orderProduct;
        }
    }
}
