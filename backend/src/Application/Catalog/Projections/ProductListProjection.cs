namespace Application.Catalog.Projections;

public sealed record ProductListProjection(Guid Id, string Name, decimal Price, int Stock);