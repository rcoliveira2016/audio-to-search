using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AudioToSearch.Infra.Data.UnitOfWorks;

public interface IUnitOfWork: IDisposable
{
    public IDbContextTransaction BeginTransaction();
    public Task Commit();
}
