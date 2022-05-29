using Domain.Common.ValueObjects;

namespace Domain.Common.Entities;

public abstract class Entity
{
    private protected Entity(EntityId id)
    {
        Id = id;
    }

    internal EntityId Id { get; }
}