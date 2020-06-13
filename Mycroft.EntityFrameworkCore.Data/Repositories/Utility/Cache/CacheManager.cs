using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Mycroft.EntityFrameworkCore.Core.Models.Admin;
using Mycroft.EntityFrameworkCore.Core.Models.Approval;
using Mycroft.EntityFrameworkCore.Core.Utility;
using Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.Repository.Utility.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly DataEngineDbContext _context;
        private readonly IMemoryCache _cache;

        public CacheManager(DataEngineDbContext context, IMemoryCache memoryCache)
        {
            this._context = context;
            this._cache = memoryCache;
        }

        public async Task<List<Core.Models.Approval.Action>> AllActions(bool reload = false)
        {
            if (reload)
            {
                return _cache.Set(Constants.ALL_ACTIONS, await _context.Actions.AsNoTracking().ToListAsync(), TimeSpan.FromDays(7));
            }
            else
                return await _cache.GetOrCreateAsync(Constants.ALL_ACTIONS, entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromDays(7);
                    return _context.Actions.AsNoTracking().ToListAsync();
                });
        }

        public async Task<Core.Models.Approval.Action> Actions(long Id) => (await AllActions()).SingleOrDefault(x => x.Id == Id);

        public async Task<Core.Models.Approval.Action> Actions(string actionName) => (await AllActions()).SingleOrDefault(x => x.Name == actionName);

        public async Task<List<AdminRole>> AllRoles(bool reload = false)
        {
            if (reload)
            {
                return _cache.Set(Constants.ALL_ROLES, await _context.AdminRoles.AsNoTracking().ToListAsync(), TimeSpan.FromDays(7));
            }
            else
                return await _cache.GetOrCreateAsync(Constants.ALL_ROLES, entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromDays(7);
                    return _context.AdminRoles.AsNoTracking().ToListAsync();
                });
        }

        public async Task<ApprovalConfiguration> ApprovalConfiguration(long Id) => (await AllApprovalConfigurations()).SingleOrDefault(p => p.Id == Id);

        public async Task<List<ApprovalConfiguration>> AllApprovalConfigurations(bool reload = false)
        {
            if (reload)
            {
                return _cache.Set(Constants.ALL_APPROVAL_CONFIGURATIONS, await _context.ApprovalConfigurations.AsNoTracking().ToListAsync(), TimeSpan.FromDays(7));
            }
            else
                return await _cache.GetOrCreateAsync(Constants.ALL_APPROVAL_CONFIGURATIONS, entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromDays(7);
                    return _context.ApprovalConfigurations.AsNoTracking().ToListAsync();
                });
        }

        public async Task<AdminRole> Role(long Id) => (await AllRoles()).SingleOrDefault(p => p.Id == Id);

        public async Task<AdminRole> Role(string code) => (await AllRoles()).SingleOrDefault(p => p.Code == code);

        public async Task<AdminRole> RoleByName(string name) => (await AllRoles()).SingleOrDefault(p => p.Name == name);
    }
}