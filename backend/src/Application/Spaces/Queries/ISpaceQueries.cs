using Application.Spaces.Projections;

namespace Application.Spaces.Queries;

public interface ISpaceQueries
{
    Task<List<SpaceListProjection>> GetSpaces(int pageIndex, int pageSize);
    Task<long> GetTotalAmount();
}