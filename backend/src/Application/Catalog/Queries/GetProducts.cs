using Application.Catalog.Projections;
using MediatR;

namespace Application.Catalog.Queries;

public static class GetProducts
{
    public sealed class Query : IRequest<Result>
    {
        public Query(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
    }

    public sealed class Result
    {
        public Result(List<ProductListProjection> products, long totalAmount)
        {
            Products = products;
            TotalAmount = totalAmount;
        }

        public List<ProductListProjection> Products { get; }
        public long TotalAmount { get; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result>
    {
        private readonly IProductQueries _productQueries;

        public Handler(IProductQueries productQueries)
        {
            _productQueries = productQueries;
        }

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await _productQueries.GetProducts(request.PageIndex, request.PageSize);
            var totalAmount = await _productQueries.GetTotalAmount();

            return new(products, totalAmount);
        }
    }
}