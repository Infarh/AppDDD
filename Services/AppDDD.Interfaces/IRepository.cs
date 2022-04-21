using AppDDD.Interfaces.Base.Entities;

namespace AppDDD.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    IEnumerable<T> GetAll();

    T? GetById(int id);

    int Count();

    int Add(T item);

    bool Update(T item);

    //bool Delete(T item);
    T? Delete(T item);
}