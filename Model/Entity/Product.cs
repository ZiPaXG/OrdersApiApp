namespace OrdersApiApp.Model.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Article { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name} - {Article}";
        }
    }
}