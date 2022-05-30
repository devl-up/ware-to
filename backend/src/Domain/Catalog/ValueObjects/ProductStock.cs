using Domain.Exceptions;

namespace Domain.Catalog.ValueObjects;

public sealed record ProductStock
{
    public const int MinimumAmount = 0;
    public const string MinimumAmountMessage = "Product stock can't be negative";

    public ProductStock(int value)
    {
        if (value < MinimumAmount)
        {
            throw new DomainException(MinimumAmountMessage);
        }

        Value = value;
    }

    public int Value { get; }
}