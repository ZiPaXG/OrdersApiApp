using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderProductService
{
    public interface IDaoOrderProduct
    {
        Task<List<OrderProduct>> GetAllOrderProduct();
        Task<OrderProduct> GetById(int id);
        Task<OrderProduct> AddOrderProduct(OrderProduct orderProduct);
        Task<OrderProduct> UpdateOrderProduct(OrderProduct orderProduct);
        Task<bool> DeleteOrderProduct(int id);
    }
}
