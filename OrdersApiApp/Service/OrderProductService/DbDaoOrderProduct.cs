using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderProductService
{
    public class DbDaoOrderProduct : IDaoOrderProduct
    {
        public ApplicationDbContext context;

        public DbDaoOrderProduct(ApplicationDbContext context)
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
            try
            {
                await context.Orders.LoadAsync();
                OrderProduct? orderProduct = await context.OrderProducts.FirstOrDefaultAsync(t => t.Id == id);
                // Ищем все OrderProduct, которые принадлежат одному Order
                IEnumerable<OrderProduct> productsInOrder = await context.OrderProducts.Where(t => t.OrderId == orderProduct.OrderId).ToListAsync();
                //Если он последний - удаляем и заказ
                if(productsInOrder.Count() == 1)
                {
                    Order? order = await context.Orders.FirstOrDefaultAsync(t => t.Id == orderProduct.OrderId);
                    context.Orders.Remove(order);
                }
                context.OrderProducts.Remove(orderProduct);
                await context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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

        public async Task<List<OrderProduct>> GetProductsInOrderById(int id)
        {
            List<OrderProduct> orderProducts = await context.OrderProducts.Where(t => t.OrderId == id).ToListAsync();
            await context.Orders.LoadAsync();
            await context.Products.LoadAsync();
            await context.Clients.LoadAsync();
            return orderProducts;
        }

        public async Task<List<OrderProduct>> GetByOrderId(int id)
        {
            await context.Products.LoadAsync();
            return await context.OrderProducts.Where(t => t.OrderId == id).ToListAsync();
        }

        public async Task<OrderProduct> Update(OrderProduct orderProduct)
        {
            context.OrderProducts.Update(orderProduct);
            await context.SaveChangesAsync();
            return orderProduct;
        }

        public async Task<Bill> GetBill(int id)
        {
            float totalPrice = 0;
            List<Product> products = new List<Product>();
            List<OrderProduct> orderProducts = await GetByOrderId(id);
            // Собираем продукты заказа и суммируем цену товаров
            foreach(OrderProduct orderProduct in orderProducts)
            {
                products.Add(orderProduct.Product);
                totalPrice += orderProduct.Product.Price * orderProduct.CountProduct;
            }
            Bill bill = new Bill(products, totalPrice);
            return bill;
        }
    }
}
