namespace OrdersApiApp.Model.Entity
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int CountProduct { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }

        public override string ToString()
        {
            return $"{Id} - {ProductId} - {OrderId} - {CountProduct}";
        }
    }
}
