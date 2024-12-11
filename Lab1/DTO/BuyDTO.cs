using System.ComponentModel.DataAnnotations;

namespace Lab1.DTO
{
    public class BuyDTO
    {
        [Required]
        public int ProveedorId { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        [Required]
        public bool Pagado { get; set; }

        [Required]
        public bool Entregado { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Debe incluir al menos un producto.")]
        public List<int> ProductoIds { get; set; }
    }
}
