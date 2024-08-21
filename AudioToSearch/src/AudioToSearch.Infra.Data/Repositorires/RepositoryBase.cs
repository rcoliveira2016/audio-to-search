using AudioToSearch.Domain.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AudioToSearch.Infra.Data.Repositorires;

public abstract class RepositoryBase<T>(
        AudioToSearchContext dbContext
    ) : IRepositoryBase<T> where T : class
{
    AudioToSearchContext _context = dbContext;
    protected DbSet<TType> GetDbSet<TType>() where TType : class
        => _context.Set<TType>();
    protected DbSet<T> dbSet => GetDbSet<T>();
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync<TId>(TId id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

}
