using Mycroft.EntityFrameworkCore.Core.Models;
using Mycroft.EntityFrameworkCore.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.IRepository
{
    public interface ICRUD<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> ReadSingleAsync(long Id, bool WithTracking = true, string include = null);

        Task<IEnumerable<TEntity>> ReadByIdsAsync(long[] Id, bool WithTracking = false);

        Task<IEnumerable<TEntity>> ReadAllAsync();

        Task<TEntity> CreateAsync(TEntity TEntity);

        Task<TEntity> CreateApprovedAsync(string TEntity);

        Task<bool> CreateMultipleAsync(IEnumerable<TEntity> TEntity);

        Task UpdateAsync(TEntity TEntity);

        Task UpdateApprovedAsync(string TEntity);

        Task UpdateMultipleAsync(IEnumerable<TEntity> TEntity);

        Task<bool> DeleteAsync(long Id);

        Task<bool> DeleteApprovedAsync(string Id);

        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, string[] ChildObjectNamesToInclude = null, bool WithTracking = false);

        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate, string[] ChildObjectNamesToInclude = null, bool WithTracking = false);

        Task<Paginated<TEntity>> GetWherePaginated(PaginatedQuery<TEntity> query);

        Task<long> CountAll();

        Task<long> CountWhere(Expression<Func<TEntity, bool>> predicate);
    }
}