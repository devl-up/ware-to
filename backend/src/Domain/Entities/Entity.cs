using Domain.ValueObjects;

namespace Domain.Entities;

public abstract class Entity
{
    private protected Entity(EntityId id)
    {
        Id = id;
    }

    internal EntityId Id { get; }
}