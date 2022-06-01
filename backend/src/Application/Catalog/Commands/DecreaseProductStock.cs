using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Domain.Catalog.Repositories;
using Domain.Catalog.ValueObjects;
using Domain.ValueObjects;
using MediatR;

namespace Application.Catalog.Commands;

public static class DecreaseProductStock
{
    public sealed class Command : IRequest
    {
        public Command(Guid id, int variation)
        {
            Id = id;
            Variation = variation;
        }

        [RequiredGuid(ErrorMessage = EntityId.EmptyMessage)]
        public Guid Id { get; }
        
        [Range(ProductStockVariation.MinimumAmount, int.MaxValue, ErrorMessage = ProductStockVariation.MinimumAmountMessage)]
        public int Variation { get; }
    }
    
    internal sealed class Handler : IRequestHandler<Command>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(new(request.Id));
            product.DecreaseStock(new(request.Variation));
            await _productRepository.Save(product);
            
            return Unit.Value;
        }
    }
}