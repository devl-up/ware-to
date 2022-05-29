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
        var product = new Product(
            new(id),
            new(name),
            new(price),
            new(stock)
        );

        // Assert
        var actualMemento = product.ToMemento();
        actualMemento.Should().BeEquivalentTo(expectedMemento);
    }
}