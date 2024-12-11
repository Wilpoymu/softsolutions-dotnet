
namespace Lab1.DTO
{
    public class BuyResponseDTO
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaPago { get; set; }
        public bool Pagado { get; set; }
        public bool Entregado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<ProductResponseDTO> Productos { get; set; } = new List<ProductResponseDTO>();
    }

    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
    }
}