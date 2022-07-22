using Application.Spaces.Projections;
using MediatR;

namespace Application.Spaces.Queries;

public static class GetSpaces
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

    public sealed record Result(List<SpaceListProjection> Spaces, long TotalAmount);

    internal sealed class Handler : IRequestHandler<Query, Result>
    {
        private readonly ISpaceQueries _spaceQueries;

        public Handler(ISpaceQueries spaceQueries)
        {
            _spaceQueries = spaceQueries;
        }

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var spaces = await _spaceQueries.GetSpaces(request.PageIndex, request.PageSize);
            var totalAmount = await _spaceQueries.GetTotalAmount();

            return new(spaces, totalAmount);
        }
    }
}