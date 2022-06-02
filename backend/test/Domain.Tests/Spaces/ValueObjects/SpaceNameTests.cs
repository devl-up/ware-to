using System;
using Domain.Exceptions;
using Domain.Spaces.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Spaces.ValueObjects;

public sealed class SpaceNameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_Should_ThrowDomainException_WhenValueIsEmpty(string value)
    {
        // Arrange
        const string emptyMessage = "Space name can't be empty";

        // Act
        var act = Act(value);

        // Assert
        act.Should().ThrowExactly<DomainException>().WithMessage(emptyMessage);
    }

    [Fact]
    public void Constructor_Should_ThrowDomainException_WhenValueExceedsMaximumLength()
    {
        // Arrange
        const int maximumLength = 50;
        const string maximumLengthMessage = "Space name can't exceed 50 characters";

        // Act
        var act = Act(new('*', maximumLength + 1));

        // Assert
        act.Should().ThrowExactly<DomainException>().WithMessage(maximumLengthMessage);
    }

    private static Func<SpaceName> Act(string value)
    {
        return () => new(value);
    }
}