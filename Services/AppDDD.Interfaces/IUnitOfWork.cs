using AppDDD.Interfaces.Base.Entities;

namespace AppDDD.Interfaces;

public interface IUnitOfWork<T> where T : class, IEntity
{
    bool SaveChanges();
    Task<bool> SaveChangesAsync(CancellationToken Cancel = default);
}