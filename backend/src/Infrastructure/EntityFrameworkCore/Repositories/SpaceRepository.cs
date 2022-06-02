using Domain.Spaces.Entities;
using Domain.Spaces.Mementos;
using Domain.Spaces.Repositories;
using Domain.ValueObjects;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkCore.Repositories;

internal sealed class SpaceRepository : ISpaceRepository
{
    private readonly WareToDbContext _context;

    public SpaceRepository(WareToDbContext context)
    {
        _context = context;
    }

    public async Task<Space> GetById(EntityId id)
    {
        var memento = await _context.Set<SpaceMemento>()
            .AsNoTracking()
            .FirstOrDefaultAsync(memento => memento.Id == id.Value);

        if (memento == null)
        {
            throw new InfrastructureException($"Space with id {id.Value} not found");
        }

        return Space.FromMemento(memento);
    }

    public async Task Save(Space space)
    {
        var newMemento = space.ToMemento();
        var existingMemento = await _context.Set<SpaceMemento>().FindAsync(space.Id.Value);

        if (existingMemento == null)
        {
            await _context.AddAsync(newMemento);
        }
        else
        {
            _context.Entry(existingMemento).CurrentValues.SetValues(newMemento);
        }

        await _context.SaveChangesAsync();
    }

    public Task Remove(Space space)
    {
        _context.Remove(space.ToMemento());
        return _context.SaveChangesAsync();
    }
}