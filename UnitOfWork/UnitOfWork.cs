using Microsoft.EntityFrameworkCore.Storage;
using MJRPAdmin.DBContext;
using MJRPAdmin.Repositories;
using MJRPAdmin.Repositories.Interfaces;

namespace MJRPAdmin.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly mjrpContext _dbContext;
        //private readonly IMapper _mapper;
        private IDbContextTransaction _objTran;

        private bool disposed = false;
        public IUploadResultRepository UploadResult { get; }
        public IAcadProgRepository AcadProgRepo { get; }

        public UnitOfWork(mjrpContext dbContext)
        {
            _dbContext = dbContext;
            UploadResult = new UploadResultRepository(_dbContext);
            AcadProgRepo = new AcadProgRepository(_dbContext);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();

            //try
            //{
            //  return await _dbContext.SaveChangesAsync();
            //}
            //catch (DbUpdateException ex)
            //{
            //	throw new OperationException(GenericConstants.SomethingWentWrong, ex.Message);
            //}
            //catch (OperationCanceledException ex)
            //{
            //  throw new OperationException(GenericConstants.SomethingWentWrong, ex.Message);
            //}
        }

        public bool HasChanges()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            disposed = true;
        }

        public async Task DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    await _dbContext.DisposeAsync();
                }
            }

            disposed = true;
        }

        public async Task CreateTransactionAsync()
        {
            _objTran = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _objTran.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _objTran.RollbackAsync();
            await _objTran.DisposeAsync();
        }

        public IExecutionStrategy GetExecutionStrategy()
        {
            return _dbContext.Database.CreateExecutionStrategy();
        }
    }
}
