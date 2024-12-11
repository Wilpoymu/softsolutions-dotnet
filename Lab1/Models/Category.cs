using System.Text.Json.Serialization;

namespace Lab1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Product>?products { get; set; }

	}
}
