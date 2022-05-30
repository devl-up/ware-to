using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Domain.Catalog.Entities;
using Domain.Catalog.Repositories;
using Domain.Catalog.ValueObjects;
using Domain.ValueObjects;
using MediatR;

namespace Application.Catalog.Commands;

public static class AddProduct
{
    public sealed class Command : IRequest
    {
        public Command(Guid id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        [RequiredGuid(ErrorMessage = EntityId.EmptyMessage)]
        public Guid Id { get; }

        [Required(ErrorMessage = ProductName.EmptyMessage)]
        [MaxLength(ProductName.MaximumLength, ErrorMessage = ProductName.MaximumLengthMessage)]
        public string Name { get; }
        
        [Range((double) ProductPrice.MinimumAmount, double.MaxValue, ErrorMessage = ProductPrice.MinimumAmountMessage)]
        public decimal Price { get; }
        
        [Range(ProductStock.MinimumAmount, int.MaxValue, ErrorMessage = ProductStock.MinimumAmountMessage)]
        public int Stock { get; }
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
            var product = new Product(
                new(request.Id),
                new(request.Name),
                new(request.Price),
                new(request.Stock)
            );

            await _productRepository.Save(product);

            return Unit.Value;
        }
    }
}