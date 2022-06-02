using Domain.Exceptions;

namespace Domain.Spaces.ValueObjects;

public sealed record SpaceName
{
    public const string EmptyMessage = "Space name can't be empty";
    public const int MaximumLength = 50;
    public const string MaximumLengthMessage = "Space name can't exceed 50 characters";

    public SpaceName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(EmptyMessage);
        }

        if (value.Length > MaximumLength)
        {
            throw new DomainException(MaximumLengthMessage);
        }

        Value = value;
    }

    public string Value { get; }
}