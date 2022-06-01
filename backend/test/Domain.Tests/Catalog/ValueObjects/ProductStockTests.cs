using System;
using Domain.Catalog.ValueObjects;
using Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Catalog.ValueObjects;

public sealed class ProductStockTests
{
    [Fact]
    public void Constructor_Should_ThrowDomainException_When_ValueIsNegative()
    {
        // Arrange
        const int minimumAmount = 0;
        const string minimumAmountMessage = "Product stock can't be negative";

        // Act
        var act = Act(minimumAmount - 1);

        // Assert
        act.Should().ThrowExactly<DomainException>(minimumAmountMessage);
    }

    [Fact]
    public void Increase_Should_ReturnNewStockWithAddedValue_WhenSuccessful()
    {
        // Arrange
        const int stockValue = 1;
        const int stockVariationAmount = 1;
        const int expectedStockValue = stockValue + stockVariationAmount;

        var stock = new ProductStock(stockValue);

        // Act
        var newStock = stock.Increase(new(stockVariationAmount));

        // Assert
        newStock.Value.Should().Be(expectedStockValue);
    }

    [Fact]
    public void Decrease_Should_ReturnNewStockWithSubtractedValue_WhenSuccessful()
    {
        // Arrange
        const int stockValue = 2;
        const int stockVariationAmount = 1;
        const int expectedStockValue = stockValue - stockVariationAmount;

        var stock = new ProductStock(stockValue);

        // Act
        var newStock = stock.Decrease(new(stockVariationAmount));

        // Assert
        newStock.Value.Should().Be(expectedStockValue);
    }

    [Fact]
    public void Decrease_Should_ThrowDomainException_WhenVariationIsBiggerThenValue()
    {
        // Arrange
        const int stockValue = 1;
        const int stockVariationAmount = 2;
        const string expectedMessage = "Product stock can't be negative";

        var stock = new ProductStock(stockValue);

        // Act
        var newStock = () => stock.Decrease(new(stockVariationAmount));

        // Assert
        newStock.Should().ThrowExactly<DomainException>().WithMessage(expectedMessage);
    }

    private static Func<ProductStock> Act(int value)
    {
        return () => new(value);
    }
}