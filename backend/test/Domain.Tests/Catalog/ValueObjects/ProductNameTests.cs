using System;
using Domain.Catalog.ValueObjects;
using Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Catalog.ValueObjects;

public sealed class ProductNameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_Should_ThrowDomainException_When_ValueIsEmpty(string value)
    {
        // Arrange
        const string emptyMessage = "Product name can't be empty";

        // Act
        var act = Act(value);

        // Assert
        act.Should().ThrowExactly<DomainException>(emptyMessage);
    }

    [Fact]
    public void Constructor_Should_ThrowDomainException_When_ValueExceedsMaximumLength()
    {
        // Arrange
        const int maximumLength = 50;
        const string maximumLengthMessage = "Product name can't exceed 50 characters";

        // Act
        var act = Act(new('*', maximumLength + 1));

        // Assert
        act.Should().ThrowExactly<DomainException>(maximumLengthMessage);
    }

    private static Func<ProductName> Act(string value)
    {
        return () => new(value);
    }
}