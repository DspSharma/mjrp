using System.Linq.Expressions;

namespace MJRPAdmin.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(ulong id);

        Task<T> GetByIdAsync(string id);

        Task<ICollection<T>> GetAllAsync();

        IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression);

        Task<IList<T>> GetWhereAsync(Expression<Func<T, bool>> expression);

        Task<IList<T>> GetWhereIgnoreQueryFiltersAsync(Expression<Func<T, bool>> expression);

        Task<T> GetSingleRecordAsync(Expression<Func<T, bool>> expression);

        Task<T> GetFirstRecordAsync(Expression<Func<T, bool>> expression);

        Task<bool> RecordExistsAsync(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
