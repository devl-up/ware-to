using System;
using Domain.Spaces.Entities;
using Domain.Spaces.Mementos;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Spaces.Entities;

public sealed class SpaceTests
{
    [Fact]
    public void Constructor_Should_CreateNewSpace_When_Successful()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string name = "name";

        var expectedMemento = new SpaceMemento(id, name);

        // Act
        var space = new Space(new(id), new(name));

        // Assert
        var actualMemento = space.ToMemento();
        actualMemento.Should().BeEquivalentTo(expectedMemento);
    }
}