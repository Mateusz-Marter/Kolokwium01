namespace Kolokwium01.Models;

public class NewOrder
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public int IdClient { get; set; }
}