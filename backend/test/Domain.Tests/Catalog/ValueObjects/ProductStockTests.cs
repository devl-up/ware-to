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

    private static Func<ProductStock> Act(int value)
    {
        return () => new(value);
    }
}