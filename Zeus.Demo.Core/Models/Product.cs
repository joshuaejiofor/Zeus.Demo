using System.Text.Json.Serialization;

namespace Zeus.Demo.Core.Models
{
    public class Product : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string Image { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public ProductType ProductType { get; set; }
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}