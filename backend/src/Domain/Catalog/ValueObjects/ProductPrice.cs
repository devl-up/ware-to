using Domain.Common.Exceptions;

namespace Domain.Catalog.ValueObjects;

public sealed record ProductPrice
{
    public const decimal MinimumAmount = 0;
    public const string MinimumAmountMessage = "Product price can't be negative";

    public ProductPrice(decimal value)
    {
        if (value < MinimumAmount)
        {
            throw new DomainException(MinimumAmountMessage);
        }

        Value = value;
    }

    public decimal Value { get; }
}