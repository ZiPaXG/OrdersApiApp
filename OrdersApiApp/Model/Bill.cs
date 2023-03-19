using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Model
{
    public class Bill
    {
        List<Product> products;
        float totalPrice;

        public Bill(List<Product> products, float totalPrice)
        {
            this.products = products;
            this.totalPrice = totalPrice;
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public float GetTotalPrice()
        {
            return totalPrice;
        }
    }
}
