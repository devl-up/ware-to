namespace Domain.Spaces.Mementos;

public sealed class SpaceMemento
{
    public SpaceMemento(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }
    public string Name { get; }
}