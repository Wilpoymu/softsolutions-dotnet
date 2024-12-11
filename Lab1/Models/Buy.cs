namespace Lab1.Models;

public class Buy
{
    public int Id { get; set; }
    public int ProveedorId { get; set; }
    public List<Product>? Productos { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaPago { get; set; }
    public bool Pagado { get; set; }
    public bool Entregado { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public List<BuyProduct> BuyProducts { get; set; } = new List<BuyProduct>();

    public void Validate()
    {
        if (ProveedorId <= 0)
        {
            throw new ArgumentException("El proveedor es requerido");
        }
        if (BuyProducts == null || !BuyProducts.Any())
        {
            throw new ArgumentException("Los productos son requeridos");
        }
        if (Total <= 0)
        {
            throw new ArgumentException("El total es requerido");
        }
    }
}