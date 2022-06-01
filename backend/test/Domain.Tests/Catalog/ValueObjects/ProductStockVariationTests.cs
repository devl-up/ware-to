using System;
using Domain.Catalog.ValueObjects;
using Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Catalog.ValueObjects;

public sealed class ProductStockVariationTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_Should_ThrowDomainException_When_VariationAmountIsLesserThenOne(int amount)
    {
        // Arrange
        const string expectedMessage = "Product stock variation should at least have an amount of 1";
        
        // Act
        var act = Act(amount);
        
        // Assert
        act.Should().ThrowExactly<DomainException>().WithMessage(expectedMessage);
    }

    private static Func<ProductStockVariation> Act(int amount)
    {
        return () => new (amount);
    }
}