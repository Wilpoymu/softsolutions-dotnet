using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lab1.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        
        [JsonIgnore]
        public Category? Category { get; set; }

        public List<BuyProduct> BuyProducts { get; set; } = new List<BuyProduct>();
    }
}
