using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Domain.Catalog.Repositories;
using Domain.Catalog.ValueObjects;
using MediatR;

namespace Application.Catalog.Commands;

public static class ChangeProductInformation
{
    public sealed class Command : IRequest
    {
        public Command(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        [RequiredGuid]
        public Guid Id { get; }

        [Required(ErrorMessage = ProductName.EmptyMessage)]
        [MaxLength(ProductName.MaximumLength, ErrorMessage = ProductName.MaximumLengthMessage)]
        public string Name { get; }

        [Range((double) ProductPrice.MinimumAmount, double.MaxValue, ErrorMessage = ProductPrice.MinimumAmountMessage)]
        public decimal Price { get; }
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
            product.ChangeInformation(new(request.Name), new(request.Price));
            await _productRepository.Save(product);

            return Unit.Value;
        }
    }
}