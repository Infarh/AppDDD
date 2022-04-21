using AppDDD.Interfaces.Base.Entities;

namespace AppDDD.Interfaces;

public interface IRepositoryAsync<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel = default);

    Task<T?> GetByIdAsync(int id, CancellationToken Cancel = default);

    Task<int> CountAsync(CancellationToken Cancel = default);

    Task<int> AddAsync(T item, CancellationToken Cancel = default);

    Task<bool> UpdateAsync(T item, CancellationToken Cancel = default);

    //Task<bool> DeleteAsync(T item, CancellationToken Cancel = default);
    Task<T?> DeleteAsync(T item, CancellationToken Cancel = default);
}