namespace Lab1.Models;

public class BuyProduct
{
    public int BuyId { get; set; }
    public Buy Buy { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}