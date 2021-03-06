using Domain.Catalog.Entities;
using Domain.Catalog.Mementos;
using Domain.Catalog.Repositories;
using Domain.ValueObjects;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkCore.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly WareToDbContext _context;

    public ProductRepository(WareToDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetById(EntityId id)
    {
        var memento = await _context.Set<ProductMemento>()
            .AsNoTracking()
            .FirstOrDefaultAsync(memento => memento.Id == id.Value);

        if (memento == null)
        {
            throw new InfrastructureException($"Product with id {id.Value} not found");
        }

        return Product.FromMemento(memento);
    }

    public async Task Save(Product product)
    {
        var newMemento = product.ToMemento();
        var existingMemento = await _context.Set<ProductMemento>().FindAsync(product.Id.Value);

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

    public Task Remove(Product product)
    {
        _context.Remove(product.ToMemento());
        return _context.SaveChangesAsync();
    }
}