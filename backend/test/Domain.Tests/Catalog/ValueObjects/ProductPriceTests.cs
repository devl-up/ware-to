using System;
using Domain.Catalog.ValueObjects;
using Domain.Common.Exceptions;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Catalog.ValueObjects;

public sealed class ProductPriceTests
{
    [Fact]
    public void Constructor_Should_ThrowDomainException_When_ValueIsNegative()
    {
        // Arrange
        const decimal minimumAmount = 0;
        const string minimumAmountMessage = "Product price can't be negative";

        // Act
        var act = Act(minimumAmount - 1);

        // Assert
        act.Should().ThrowExactly<DomainException>(minimumAmountMessage);
    }

    private static Func<ProductPrice> Act(decimal value)
    {
        return () => new(value);
    }
}