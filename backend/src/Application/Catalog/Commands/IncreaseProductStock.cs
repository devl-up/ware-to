using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Domain.Catalog.Repositories;
using MediatR;

namespace Application.Catalog.Commands;

public static class IncreaseProductStock
{
    public sealed class Command : IRequest
    {
        public Command(Guid id, int variation)
        {
            Id = id;
            Variation = variation;
        }

        [RequiredGuid]
        public Guid Id { get; }
        
        [Range(1, int.MaxValue)]
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
            product.IncreaseStock(new(request.Variation));
            await _productRepository.Save(product);
            
            return Unit.Value;
        }
    }
}