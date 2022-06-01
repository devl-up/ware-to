using Application.Attributes;
using Domain.Catalog.Repositories;
using Domain.ValueObjects;
using MediatR;

namespace Application.Catalog.Commands;

public static class RemoveProduct
{
    public sealed class Command : IRequest
    {
        public Command(Guid id)
        {
            Id = id;
        }

        [RequiredGuid(ErrorMessage = EntityId.EmptyMessage)]
        public Guid Id { get; }
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
            await _productRepository.Remove(product);

            return Unit.Value;
        }
    }
}