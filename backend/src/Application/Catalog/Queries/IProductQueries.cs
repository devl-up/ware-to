using Application.Catalog.Projections;

namespace Application.Catalog.Queries;

public interface IProductQueries
{
    Task<List<ProductListProjection>> GetProducts(int pageIndex, int pageSize);
    Task<long> GetTotalAmount();
}