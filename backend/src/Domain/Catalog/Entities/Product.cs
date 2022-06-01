using Domain.Catalog.Mementos;
using Domain.Catalog.ValueObjects;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Catalog.Entities;

public sealed class Product : RootEntity
{
    private ProductName _name;
    private ProductPrice _price;
    private ProductStock _stock;

    public Product(EntityId id, ProductName name, ProductPrice price, ProductStock stock) : base(id)
    {
        _name = name;
        _price = price;
        _stock = stock;
    }

    internal ProductMemento ToMemento()
    {
        return new(Id.Value, _name.Value, _price.Value, _stock.Value);
    }

    internal static Product FromMemento(ProductMemento memento)
    {
        return new(
            new(memento.Id),
            new(memento.Name),
            new(memento.Price),
            new(memento.Stock)
        );
    }

    public void ChangeInformation(ProductName name, ProductPrice price)
    {
        _name = name;
        _price = price;
    }

    public void IncreaseStock(ProductStockVariation variation)
    {
        _stock = _stock.Increase(variation);
    }

    public void DecreaseStock(ProductStockVariation variation)
    {
        _stock = _stock.Decrease(variation);
    }
}