using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mycroft.EntityFrameworkCore.Core.Helpers;
using Mycroft.EntityFrameworkCore.Data.IRepository.Admin;
using Mycroft.EntityFrameworkCore.Data.IRepository.Approval;
using Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Cache;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;
using static Mycroft.EntityFrameworkCore.Core.Models.Enumerators;

namespace Mycroft.EntityFrameworkCore.Data.Repository.Admin
{
    public class AdminRepository : CRUDApprovable<Core.Models.Admin.Admin>, IAdminRepository
    {
        public AdminRepository(DataEngineDbContext dataEngineDbContext, IHttpContextAccessor accessor, ICacheManager cacheManager, IApprovalRepository approvalRepository) : base(dataEngineDbContext, accessor, cacheManager, approvalRepository)
        {
        }

        public AdminRepository(DataEngineDbContext dataEngineDbContext) : base(dataEngineDbContext)
        {
        }

        [ApprovalAction("CreateAdmin")]
        public override async Task<Tuple<Core.Models.Admin.Admin, ApprovalStatus, string>> CreateApprovableAsync(Core.Models.Admin.Admin TEntity)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            ApprovalActionAttribute attr = typeof(AdminRepository).GetMethod(GetActualAsyncMethodName()).GetCustomAttribute<ApprovalActionAttribute>();
            string actionName = attr.ActionName;
            var action = ActionByName(actionName);
            if (await CanSelfApprove(actionName))
            {
                return new Tuple<Core.Models.Admin.Admin, ApprovalStatus, string>(await CreateAsync(TEntity), ApprovalStatus.Approved, "created successfully");
            }
            else
            {
                if (await PreCreateCheckAsync(TEntity))
                {
                    await CreateApprovalObject(TEntity, action);
                    return new Tuple<Core.Models.Admin.Admin, ApprovalStatus, string>(TEntity, ApprovalStatus.Pending, "creation logged for approval");
                }
                else
                {
                    return new Tuple<Core.Models.Admin.Admin, ApprovalStatus, string>(null, ApprovalStatus.Declined, "creation failed some prechecks");
                }
            }
        }

        [ApprovalAction("DeleteAdmin")]
        public override async Task<Tuple<bool, ApprovalStatus, string>> DeleteApprovableAsync(long Id)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            ApprovalActionAttribute attr = (ApprovalActionAttribute)method.GetCustomAttributes(typeof(ApprovalActionAttribute), true)[0];
            string actionName = attr.ActionName;
            var action = ActionByName(actionName);
            if (await CanSelfApprove(actionName))
            {
                return new Tuple<bool, ApprovalStatus, string>((await DeleteAsync(Id)), ApprovalStatus.Approved, "deleted successfully");
            }
            else
            {
                if (await PreDeleteCheckAsync(Id))
                {
                    await CreateApprovalObject(Id, action);
                    return new Tuple<bool, ApprovalStatus, string>(true, ApprovalStatus.Pending, "acount creation logged for approval");
                }
                else
                {
                    return new Tuple<bool, ApprovalStatus, string>(false, ApprovalStatus.Declined, "acount deletion falied prechecks");
                }
            }
        }

        public override async Task<bool> PreCreateCheckAsync(Core.Models.Admin.Admin TEntity)
        {
            if (await _context.Admins.AnyAsync(x => x.Adminlogin.username == TEntity.Adminlogin.username || x.Email == TEntity.Email || x.PrimaryPhoneNumber == TEntity.PrimaryPhoneNumber))
                return false;
            else
                return true;
        }

        public override Task<bool> PreDeleteCheckAsync(long Id)
        {
            return Task.FromResult(true);
        }

        public override async Task<bool> PreUpdateCheckAsync(Core.Models.Admin.Admin TEntity)
        {
            if (await _context.Admins.AnyAsync(x => x.Email == TEntity.Email || x.PrimaryPhoneNumber == TEntity.PrimaryPhoneNumber))
                return false;
            else
                return true;
        }

        [ApprovalAction("UpdateAdmin")]
        public override async Task<Tuple<ApprovalStatus, string>> UpdateApprovableAsync(Core.Models.Admin.Admin TEntity)
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            ApprovalActionAttribute attr = (ApprovalActionAttribute)method.GetCustomAttributes(typeof(ApprovalActionAttribute), true)[0];
            string actionName = attr.ActionName;
            var action = ActionByName(actionName);
            if (await CanSelfApprove(actionName))
            {
                await UpdateAsync(TEntity);
                return new Tuple<ApprovalStatus, string>(ApprovalStatus.Approved, "update successful");
            }
            else
            {
                if (await PreUpdateCheckAsync(TEntity))
                {
                    await CreateApprovalObject(TEntity, action, true);
                    return new Tuple<ApprovalStatus, string>(ApprovalStatus.Pending, "update successfully logged for approval");
                }
                else
                {
                    return new Tuple<ApprovalStatus, string>(ApprovalStatus.Declined, "The update failed some PreChecks.");
                }
            }
        }

        public override async Task<bool> PreCreateApprovalCheckAsync(string TEntity)
        {
            var entity = JsonConvert.DeserializeObject<Core.Models.Admin.Admin>(TEntity);
            return PreCreateCheckAsync(entity).Result;
        }

        public override async Task<bool> PreUpdateApprovalCheckAsync(string TEntity)
        {
            var entity = JsonConvert.DeserializeObject<Core.Models.Admin.Admin>(TEntity);
            return await PreUpdateCheckAsync(entity);
        }

        public override async Task<bool> PreDeleteApprovalCheckAsync(string Id)
        {
            return await PreDeleteCheckAsync(long.Parse(Id));
        }
    }
}