namespace Kolokwium01.Models;

public class Order
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public int IdClient { get; set; }
    public List<Product>? Products { get; set; }
}