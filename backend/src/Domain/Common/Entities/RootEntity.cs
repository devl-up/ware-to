using Domain.Common.ValueObjects;

namespace Domain.Common.Entities;

public abstract class RootEntity : Entity
{
    private protected RootEntity(EntityId id) : base(id)
    {
    }
}