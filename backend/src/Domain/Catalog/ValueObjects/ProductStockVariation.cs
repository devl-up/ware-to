using Domain.Exceptions;

namespace Domain.Catalog.ValueObjects;

public sealed record ProductStockVariation
{
    public const int MinimumAmount = 1;
    public const string MinimumAmountMessage = "Product stock variation should at least have an amount of 1";

    public ProductStockVariation(int amount)
    {
        if (amount < MinimumAmount)
        {
            throw new DomainException(MinimumAmountMessage);
        }

        Amount = amount;
    }

    public int Amount { get; }
}