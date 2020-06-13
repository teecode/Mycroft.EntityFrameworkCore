using Microsoft.EntityFrameworkCore;
using Mycroft.EntityFrameworkCore.Core.Models.Admin;
using Mycroft.EntityFrameworkCore.Core.Utility.Security;
using Mycroft.EntityFrameworkCore.Data.IRepository.Admin;
using Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Logger;
using System;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.Repository.Admin
{
    public class AdminLoginRepository : CRUD<AdminLogin>, IAdminLoginRepository
    {
        private readonly ILoggerManager _logger;

        public AdminLoginRepository(DataEngineDbContext dataEngineDbContext, ILoggerManager loggerManager) : base(dataEngineDbContext)
        {
            this._logger = loggerManager;
        }

        public async Task<Tuple<bool, Core.Models.Admin.AdminLogin>> AuthenticateAdmin(Core.Models.Admin.AdminLogin adminLogin)
        {
            try
            {
                var adminsFromSource = await _context.AdminLogins.Include(x => x.Admin).Include(x => x.AdminRole).SingleOrDefaultAsync(x => x.username == adminLogin.username);
                if (adminsFromSource == null)
                    return new Tuple<bool, Core.Models.Admin.AdminLogin>(false, null);

                if (PasswordHash.ValidatePassword(adminLogin.password, adminsFromSource.password))
                    return new Tuple<bool, Core.Models.Admin.AdminLogin>(true, adminsFromSource);
                else
                    return new Tuple<bool, Core.Models.Admin.AdminLogin>(false, null);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new Tuple<bool, Core.Models.Admin.AdminLogin>(false, null);
            }
        }
    }
}