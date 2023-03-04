using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderService
{
    public interface IDaoOrder
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetById(int id);
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
    }
}
