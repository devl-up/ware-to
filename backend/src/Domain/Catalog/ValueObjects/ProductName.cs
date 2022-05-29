using Domain.Common.Exceptions;

namespace Domain.Catalog.ValueObjects;

public sealed record ProductName
{
    public const string EmptyMessage = "Product name can't be empty";
    public const int MaximumLength = 50;
    public const string MaximumLengthMessage = "Product name can't exceed 50 characters";

    public ProductName(string value)
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