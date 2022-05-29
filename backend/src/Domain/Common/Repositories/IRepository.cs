using Domain.Common.Entities;
using Domain.Common.ValueObjects;

namespace Domain.Common.Repositories;

public interface IRepository<TRootEntity, TState> where TRootEntity : RootEntity
{
    Task<TRootEntity> GetById(EntityId id);
    Task Save(TRootEntity rootEntity);
}