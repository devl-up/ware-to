using Application.Spaces.Projections;
using Application.Spaces.Queries;
using Domain.Spaces.Mementos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkCore.Queries;

internal sealed class SpaceQueries : ISpaceQueries
{
    private readonly WareToDbContext _context;

    public SpaceQueries(WareToDbContext context)
    {
        _context = context;
    }

    public Task<List<SpaceListProjection>> GetSpaces(int pageIndex, int pageSize)
    {
        return _context.Set<SpaceMemento>()
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .Select(space => new SpaceListProjection(space.Id, space.Name))
            .ToListAsync();
    }

    public Task<long> GetTotalAmount()
    {
        return _context.Set<SpaceMemento>()
            .LongCountAsync();
    }
}