using Domain.Common.Exceptions;

namespace Domain.Common.ValueObjects;

public record EntityId
{
    public const string EmptyMessage = "Id can't be empty";

    public EntityId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException(EmptyMessage);
        }

        Value = value;
    }

    public Guid Value { get; }
}