using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderProductService
{
    public interface IDaoOrderProduct : IDaoTemplate<OrderProduct>
    {
        Task<List<OrderProduct>> GetProductsInOrderById(int id);
        Task<List<OrderProduct>> GetByOrderId(int id);
        Task<Bill> GetBill(int id);
    }
}
