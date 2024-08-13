using System.Security.Cryptography;

namespace AudioToSearch.Domain.Common.Repositories;

public interface IRepositoryBase <T> where T : class
{
    Task AddAsync(T entity);
    void Delete(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync<TId>(TId id);
    void Update(T entity);
}
