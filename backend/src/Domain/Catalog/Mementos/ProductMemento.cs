namespace Domain.Catalog.Mementos;

public sealed class ProductMemento
{
    public ProductMemento(Guid id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; }
}