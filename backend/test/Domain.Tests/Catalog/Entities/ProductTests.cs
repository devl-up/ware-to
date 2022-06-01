using System;
using Domain.Catalog.Entities;
using Domain.Catalog.Mementos;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Catalog.Entities;

public sealed class ProductTests
{
    [Fact]
    public void Constructor_Should_CreateNewProduct_When_Successful()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string name = "name";
        const decimal price = 1;
        const int stock = 1;

        var expectedMemento = new ProductMemento(id, name, price, stock);

        // Act
        var product = new Product(new(id), new(name), new(price), new(stock));

        // Assert
        var actualMemento = product.ToMemento();
        actualMemento.Should().BeEquivalentTo(expectedMemento);
    }

    [Fact]
    public void ChangeInformation_Should_ChangeProductInformation_WhenSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string name = "name";
        const decimal price = 1;
        const int stock = 1;

        const string expectedName = "expectedName";
        const decimal expectedPrice = 2;

        var product = new Product(new(id), new(name), new(price), new(stock));
        var expectedMemento = new ProductMemento(id, expectedName, expectedPrice, stock);

        // Act
        product.ChangeInformation(new(expectedName), new(expectedPrice));

        // Assert
        var actualMemento = product.ToMemento();
        actualMemento.Should().BeEquivalentTo(expectedMemento);
    }

    [Fact]
    public void IncreaseStock_Should_AddStockVariationAmount_WhenSuccessful()
    {
        // Arrange
        const int stock = 1;
        const int stockVariationAmount = 1;
        const int expectedStock = stock + stockVariationAmount;

        var product = new Product(new(Guid.NewGuid()), new("name"), new(1), new(stock));

        // Act
        product.IncreaseStock(new(stockVariationAmount));

        // Assert
        var memento = product.ToMemento();
        expectedStock.Should().Be(memento.Stock);
    }

    [Fact]
    public void DecreaseStock_Should_SubtractStockVariationAmount_WhenSuccessful()
    {
        // Arrange
        const int stock = 2;
        const int stockVariationAmount = 1;
        const int expectedStock = stock - stockVariationAmount;

        var product = new Product(new(Guid.NewGuid()), new("name"), new(1), new(stock));

        // Act
        product.DecreaseStock(new(stockVariationAmount));

        // Assert
        var memento = product.ToMemento();
        expectedStock.Should().Be(memento.Stock);
    }
}