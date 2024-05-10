using DataAccess.Models.User;

namespace DataAccess.Repositories.Interfaces;

public interface IRepository<T>
{
    public void Add(T entity);
    
    public void Update(T entity);
    
    public T GetFirstOrDefault(Func<T, bool> query);
    
    public IEnumerable<T> GetByQuery(Func<T, bool> query);
    
    public void Remove(T entity);
}