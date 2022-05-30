using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface IRepository<TRootEntity> where TRootEntity : RootEntity
{
    Task<TRootEntity> GetById(EntityId id);
    Task Save(TRootEntity rootEntity);
}