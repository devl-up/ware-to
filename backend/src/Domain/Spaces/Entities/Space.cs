using Domain.Entities;
using Domain.Spaces.Mementos;
using Domain.Spaces.ValueObjects;
using Domain.ValueObjects;

namespace Domain.Spaces.Entities;

public sealed class Space : RootEntity
{
    private readonly EntityId _id;
    private readonly SpaceName _name;

    public Space(EntityId id, SpaceName name) : base(id)
    {
        _id = id;
        _name = name;
    }

    internal static Space FromMemento(SpaceMemento memento)
    {
        return new(new(memento.Id), new(memento.Name));
    }

    internal SpaceMemento ToMemento()
    {
        return new(_id.Value, _name.Value);
    }
}