namespace Application.Catalog.Projections;

public record ProductListProjection(Guid Id, string Name, decimal Price, int Stock);