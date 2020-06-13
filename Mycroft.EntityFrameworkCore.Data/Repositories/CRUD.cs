using Mycroft.EntityFrameworkCore.Data.IRepository;
using Mycroft.EntityFrameworkCore.Core.Models;
using Mycroft.EntityFrameworkCore.Core.Utility;
using Mycroft.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data
{
    public class CRUD<TEntity> : ICRUD<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataEngineDbContext _context;

        public CRUD(DataEngineDbContext dataEngineDbContext)
        {
            this._context = dataEngineDbContext;
        }

        public async Task<long> CountAll() => await _context.Set<TEntity>().CountAsync();

        public async Task<long> CountWhere(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().CountAsync(predicate);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var returnItem = await _context.Set<TEntity>().AddAsync(entity);
            return returnItem.Entity;
        }

        public async Task<bool> CreateMultipleAsync(IEnumerable<TEntity> entity)
        {
            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                //log error info to entral logging system.
                return false;
            }
        }

        /// <summary>
        /// Deletes an entity using the long identifier
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long Id)
        {
            try
            {
                TEntity entity = await ReadSingleAsync(Id);
                _context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                //log error info to entral logging system.
                return false;
            }
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, string[] ChildObjectNamesToInclude = null, bool WithTracking = false)
        {
            TEntity data;
            if (ChildObjectNamesToInclude != null && ChildObjectNamesToInclude.Count() > 0)
            {
                var queryable = _context.Set<TEntity>().AsQueryable();
                foreach (var item in ChildObjectNamesToInclude)
                {
                    queryable = queryable.Include(item);
                }
                if (WithTracking)
                    data = await queryable.SingleOrDefaultAsync(predicate);
                else
                    data = await queryable.AsNoTracking().SingleOrDefaultAsync(predicate);
            }
            else
            {
                if (WithTracking)
                    data = await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
                else
                    data = await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate);
            }
            return data;
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate, string[] ChildObjectNamesToInclude = null, bool WithTracking = false)
        {
            List<TEntity> data = new List<TEntity>();
            if (ChildObjectNamesToInclude != null && ChildObjectNamesToInclude.Count() > 0)
            {
                var queryable = _context.Set<TEntity>().AsQueryable();
                foreach (var item in ChildObjectNamesToInclude)
                {
                    queryable = queryable.Include(item);
                }
                if (WithTracking)
                    data = await queryable.Where(predicate).ToListAsync();
                else
                    data = await queryable.Where(predicate).AsNoTracking().ToListAsync();
            }
            else
            {
                if (WithTracking)
                    data = await _context.Set<TEntity>().Where(predicate).ToListAsync();
                else
                    data = await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
            }
            return data;
        }

        public async Task<Paginated<TEntity>> GetWherePaginated(PaginatedQuery<TEntity> query)
        {
            int start = (query.Page - 1) * query.PageSize;
            List<TEntity> data = new List<TEntity>();
            if (query.Order == Enumerators.Order.Ascending)
            {
                if (query.ChildObjectNamesToInclude != null && query.ChildObjectNamesToInclude.Count() > 0)
                {
                    var queryable = _context.Set<TEntity>().AsQueryable();
                    foreach (var item in query.ChildObjectNamesToInclude)
                    {
                        queryable= queryable.Include(item);
                    }
                    queryable = queryable.Where(query.predicate).OrderBy(x => x.Id).AsNoTracking().Skip(start).Take(query.PageSize);
                    data = await queryable.ToListAsync();
                }
                else
                {
                    data = await _context.Set<TEntity>().Where(query.predicate).OrderBy(x => x.Id).AsNoTracking().Skip(start).Take(query.PageSize).ToListAsync();
                }
            }
            else
            {
                if (query.ChildObjectNamesToInclude != null && query.ChildObjectNamesToInclude.Count() > 0)
                {
                    var queryable = _context.Set<TEntity>().AsQueryable();
                    foreach (var item in query.ChildObjectNamesToInclude)
                    {
                        queryable = queryable.Include(item);
                    }
                    data = await queryable.Where(query.predicate).OrderByDescending(x => x.Id).AsNoTracking().Skip(start).Take(query.PageSize).ToListAsync();
                }
                else
                {
                    data = await _context.Set<TEntity>().Where(query.predicate).OrderByDescending(x => x.Id).AsNoTracking().Skip(start).Take(query.PageSize).ToListAsync();
                }
            }
            var count = await CountWhere(query.predicate);
            return new Paginated<TEntity> { Page = query.Page, PageSize = query.PageSize, Data = data, TotalCount = count };
        }

        public async Task<IEnumerable<TEntity>> ReadAllAsync()
        {
            var returnItem = await _context.Set<TEntity>().AsNoTracking().ToListAsync<TEntity>();
            return returnItem;
        }

        public async Task<IEnumerable<TEntity>> ReadByIdsAsync(long[] Id, bool WithTracking = false)
        {
            IEnumerable<TEntity> returnItem;
            if (WithTracking)
                returnItem = await _context.Set<TEntity>().Where(a => Id.Contains(a.Id)).ToListAsync<TEntity>();
            returnItem = await _context.Set<TEntity>().AsNoTracking().Where(a => Id.Contains(a.Id)).ToListAsync<TEntity>();
            return returnItem;
        }

        /// <summary>
        /// Return a single search result satisfing the search Id
        /// </summary>
        /// <param name="Id">The Search id</param>
        /// <param name="WithTracking">This indicates if the search if for update or not hence the object with the tracked.</param>
        /// <returns></returns>
        public async Task<TEntity> ReadSingleAsync(long Id, bool WithTracking = true, string include = null)
        {
            TEntity returnItem;
            if (WithTracking)
                if (!string.IsNullOrEmpty(include))
                    returnItem = await _context.Set<TEntity>().Include(include).SingleOrDefaultAsync(x => x.Id == Id);
                else
                    returnItem = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == Id);
            else
            {
                if (!string.IsNullOrEmpty(include))
                    returnItem = await _context.Set<TEntity>().Include(include).AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id);
                else
                    returnItem = await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id);
            }
            return returnItem;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _context.Entry(entity).State = EntityState.Modified);
        }

        public async Task UpdateMultipleAsync(IEnumerable<TEntity> entity)
        {
            foreach (var item in entity)
            {
                await Task.Run(() => _context.Entry(item).State = EntityState.Modified);
            }
        }

        public async Task<TEntity> CreateApprovedAsync(string TEntity)
        {
            var entity = JsonConvert.DeserializeObject<TEntity>(TEntity);
            return await CreateAsync(entity);
        }

        public async Task UpdateApprovedAsync(string TEntity)
        {
            var entity = JsonConvert.DeserializeObject<TEntity>(TEntity);
            await UpdateAsync(entity);
        }

        public async Task<bool> DeleteApprovedAsync(string Id)
        {
            return await DeleteAsync(long.Parse(Id));
        }
    }
}