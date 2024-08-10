using Microsoft.EntityFrameworkCore.Storage;

namespace AudioToSearch.Infra.Data.UnitOfWorks;

public class UnitOfWork(
        AudioToSearchContext dbContext
    ) : IUnitOfWork
{
    AudioToSearchContext _context = dbContext;

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        _context.Dispose();
    }

    void IDisposable.Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
