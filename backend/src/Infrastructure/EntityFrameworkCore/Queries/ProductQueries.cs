using Application.Catalog.Projections;
using Application.Catalog.Queries;
using Domain.Catalog.Mementos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkCore.Queries;

internal sealed class ProductQueries : IProductQueries
{
    private readonly WareToDbContext _context;

    public ProductQueries(WareToDbContext context)
    {
        _context = context;
    }

    public Task<List<ProductListProjection>> GetProducts(int pageIndex, int pageSize)
    {
        return _context.Set<ProductMemento>()
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .Select(product => new ProductListProjection(product.Id, product.Name, product.Price, product.Stock))
            .ToListAsync();
    }

    public Task<long> GetTotalAmount()
    {
        return _context.Set<ProductMemento>()
            .LongCountAsync();
    }
}