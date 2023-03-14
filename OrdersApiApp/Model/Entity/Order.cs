using System.Text.Json.Serialization;

namespace OrdersApiApp.Model.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        [JsonIgnore]
        public ICollection<OrderProduct> OrderProducts { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }

        public Order()
        {
            Description = "";
        }

        public override string ToString()
        {
            return $"{Id} - {Description} - {ClientId}";
        }
    }
}
