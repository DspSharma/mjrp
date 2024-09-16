using Microsoft.EntityFrameworkCore.Storage;
using MJRPAdmin.Repositories.Interfaces;

namespace MJRPAdmin.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUploadResultRepository UploadResult { get; }
        IAcadProgRepository AcadProgRepo { get; }

        int Save();

        Task<int> SaveAsync();

        Task DisposeAsync();

        bool HasChanges();

        Task CreateTransactionAsync();

        Task RollbackAsync();

        Task CommitTransactionAsync();

        IExecutionStrategy GetExecutionStrategy();
    }
}
