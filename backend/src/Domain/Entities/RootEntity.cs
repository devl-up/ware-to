using Domain.ValueObjects;

namespace Domain.Entities;

public abstract class RootEntity : Entity
{
    private protected RootEntity(EntityId id) : base(id)
    {
    }
}