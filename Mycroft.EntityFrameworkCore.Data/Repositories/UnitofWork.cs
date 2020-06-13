using Microsoft.EntityFrameworkCore.Storage;
using Mycroft.EntityFrameworkCore.Data.IRepository;
using System;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DataEngineDbContext _context;

        public UnitofWork(DataEngineDbContext dataEngineDbContext)
        {
            this._context = dataEngineDbContext;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task Refresh()
        {
            try
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SubmitChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}