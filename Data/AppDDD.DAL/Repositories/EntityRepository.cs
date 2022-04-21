using AppDDD.DAL.Context;
using AppDDD.Interfaces;
using AppDDD.Interfaces.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppDDD.DAL.Repositories;

public class EntityRepository<T> : IRepositoryAsync<T> where T : class, IEntity
{
    private readonly AppDB _db;
    private readonly ILogger<EntityRepository<T>> _Logger;

    public EntityRepository(AppDB db, ILogger<EntityRepository<T>> Logger)
    {
        _db = db;
        _Logger = Logger;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel = default)
    {
        var items = await _db
           .Set<T>()
           .ToArrayAsync(Cancel)
           .ConfigureAwait(false);
        return items;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken Cancel = default)
    {
        var item = await _db.Set<T>().FirstOrDefaultAsync(o => o.Id == id, Cancel).ConfigureAwait(false);
        //var item = await _db.Set<T>().FirstAsync(o => o.Id == id, Cancel).ConfigureAwait(false);
        //var item = await _db.Set<T>().SingleAsync(o => o.Id == id, Cancel).ConfigureAwait(false);
        //var item = await _db.Set<T>().SingleOrDefaultAsync(o => o.Id == id, Cancel).ConfigureAwait(false);
        //var item = await _db.Set<T>().FindAsync(id, Cancel).ConfigureAwait(false);
        return item;
    }

    public async Task<int> CountAsync(CancellationToken Cancel = default)
    {
        var resutl = await _db.Set<T>().CountAsync(Cancel).ConfigureAwait(false);
        return resutl;
    }

    public async Task<int> AddAsync(T item, CancellationToken Cancel = default)
    {
        await _db.Set<T>().AddAsync(item, Cancel).ConfigureAwait(false);
        //await _db.AddAsync(item, Cancel).ConfigureAwait(false);
        //_db.Entry(item).State = EntityState.Added;
        await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        return item.Id;
    }

    public async Task<bool> UpdateAsync(T item, CancellationToken Cancel = default)
    {
        //_db.Set<T>().Update(item);
        //_db.Update(item);
        _db.Entry(item).State = EntityState.Modified;

        //var db_item = await GetByIdAsync(item.Id, Cancel).ConfigureAwait(false);
        //if (db_item is null)
        //    return false;

        //// скопировать данные из item в db_item

        return await _db.SaveChangesAsync(Cancel) > 0;
    }

    public async Task<T?> DeleteAsync(T item, CancellationToken Cancel = default)
    {
        //_db.Set<T>().Remove(item);
        //_db.Remove(item);
        //_db.Entry(item).State = EntityState.Deleted;

        if (!await _db.Set<T>().AnyAsync(i => i.Id == item.Id, Cancel).ConfigureAwait(false)) 
            return null;

        _db.Entry(item).State = EntityState.Deleted;
        await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        return item;
    }
}
