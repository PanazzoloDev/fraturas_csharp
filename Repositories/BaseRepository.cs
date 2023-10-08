
using Microsoft.EntityFrameworkCore;

namespace fraturas_csharp.Repositories;
public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    IEnumerable<T> Where(Func<T, bool> expression);
    bool Any(Func<T, bool> expression);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public IEnumerable<T> Where(Func<T, bool> expression)
    {
        return _dbSet.Where(expression);
    }
    
    public bool Any(Func<T, bool> expression)
    {
        return _dbSet.Any(expression);
    }
}

