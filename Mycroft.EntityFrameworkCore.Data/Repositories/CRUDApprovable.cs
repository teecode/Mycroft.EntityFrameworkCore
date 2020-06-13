using Microsoft.AspNetCore.Http;
using Mycroft.EntityFrameworkCore.Core.Models;
using Mycroft.EntityFrameworkCore.Core.Models.Approval;
using Mycroft.EntityFrameworkCore.Data.IRepository;
using Mycroft.EntityFrameworkCore.Data.IRepository.Approval;
using Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Cache;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using static Mycroft.EntityFrameworkCore.Core.Models.Enumerators;

namespace Mycroft.EntityFrameworkCore.Data
{
    public class CRUDApprovable<TEntity> : CRUD<TEntity>, IApprovable<TEntity> where TEntity : BaseEntity
    {
        protected readonly ClaimsPrincipal _user;
        protected readonly ICacheManager _cache;
        protected readonly IApprovalRepository _approvalRepository;

        public CRUDApprovable(DataEngineDbContext dataEngineDbContext, IHttpContextAccessor accessor, ICacheManager cacheManager, IApprovalRepository approvalRepository) : base(dataEngineDbContext)
        {
            this._user = accessor.HttpContext.User;
            this._cache = cacheManager;
            this._approvalRepository = approvalRepository;
        }

        public CRUDApprovable(DataEngineDbContext dataEngineDbContext) : base(dataEngineDbContext)
        {
        }

        public bool isAuthenticated { get { return _user.Identity.IsAuthenticated; } }

        /// <summary>
        /// Current logged in Admin
        /// </summary>
        public long AdminID { get { return long.Parse((_user.Identity as ClaimsIdentity).FindFirst("AdminId").Value); } }

        /// <summary>
        /// Currently logged in agent
        /// </summary>
        public long AgentID { get { return long.Parse((_user.Identity as ClaimsIdentity).FindFirst("AgentId").Value); } }

        /// <summary>
        /// Currently logged in TErminal
        /// </summary>
        public long TerminalID { get { return long.Parse((_user.Identity as ClaimsIdentity).FindFirst("TerminalId").Value); } }

        public string Role { get { return (_user.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Role).Value; } }
        public bool IsAdministrativeRole { get { return (!Role.ToLower().Equals("terminal") && !Role.ToLower().Equals("agent")); } }
        public bool IsAgent { get { return Role.ToLower().Equals("agent"); } }
        public bool IsTerminal { get { return Role.ToLower().Equals("terminal"); } }

        public Core.Models.Approval.Action ActionByName(string actionname) => _cache.Actions(actionname).Result;

        public async Task<bool> CanSelfApprove(string actionName)
        {
            var action = await _cache.Actions(actionName);
            var role = await _cache.RoleByName(Role);
            var approvalConfig = await ApprovalConfigurationByActionId(action.Id);
            if (action == null)
                throw new Exception("action doesnt exist");
            if (role == null)
                throw new Exception("role doesnt exist");
            if (approvalConfig == null)
                throw new Exception("no configuration for action specified");
            if (approvalConfig.ApprovingRoleId == role.Id || approvalConfig.ApprovingUserId == AdminID)
                return true;
            else
                return false;
        }

        public long CreatedBy
        {
            get
            {
                if (IsAdministrativeRole)
                    return AdminID;
                else if (IsAgent)
                    return AgentID;
                else if (IsTerminal)
                    return TerminalID;
                else //Anonymous
                    return -1;
            }
        }

        public USERTYPE CreatedByUserType
        {
            get
            {
                if (IsAdministrativeRole)
                    return USERTYPE.ADMIN;
                else if (IsAgent)
                    return USERTYPE.AGENT;
                else if (IsTerminal)
                    return USERTYPE.TERMINAL;
                else //Anonymous
                    return USERTYPE.ANONYMOUS;
            }
        }

        private async Task<ApprovalConfiguration> ApprovalConfigurationByActionId(long actionId)
        {
            return (await _cache.AllApprovalConfigurations()).SingleOrDefault(x => x.ActionId == actionId);
        }

        protected static string GetActualAsyncMethodName([CallerMemberName]string name = null) => name;

        protected async Task CreateApprovalObject(TEntity TEntity, Core.Models.Approval.Action action, bool isUpdate = false)
        {
            Approval approval = new Approval
            {
                Assembly = string.IsNullOrEmpty(action.Assembly) ? Assembly.GetExecutingAssembly().FullName : action.Assembly,
                ActionClass = string.IsNullOrEmpty(action.ActionClass) ? this.GetType().Name : action.ActionClass,
                ActionMethod = action.ActionMethod,
                LoggedBy = CreatedBy,
                CreatedByUserType = CreatedByUserType,
                CurrentObject = JsonConvert.SerializeObject(TEntity),
                ApprovalConfigurationId = (await ApprovalConfigurationByActionId(action.Id)).Id,
            };

            if (isUpdate)
                approval.PreviousObject = JsonConvert.SerializeObject(await ReadSingleAsync(TEntity.Id));
            await _approvalRepository.CreateAsync(approval);
        }

        protected async Task CreateApprovalObject(long Id, Core.Models.Approval.Action action)
        {
            Core.Models.Approval.Approval approval = new Core.Models.Approval.Approval
            {
                Assembly = string.IsNullOrEmpty(action.Assembly) ? Assembly.GetExecutingAssembly().FullName : action.Assembly,
                ActionClass = string.IsNullOrEmpty(action.ActionClass) ? this.GetType().Name : action.ActionClass,
                ActionMethod = action.ActionMethod,
                LoggedBy = CreatedBy,
                CreatedByUserType = CreatedByUserType,
                CurrentObject = Id.ToString()
            };
            await _approvalRepository.CreateAsync(approval);
        }

        public virtual Task<Tuple<TEntity, ApprovalStatus, string>> CreateApprovableAsync(TEntity TEntity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Tuple<ApprovalStatus, string>> UpdateApprovableAsync(TEntity TEntity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Tuple<bool, ApprovalStatus, string>> DeleteApprovableAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> PreCreateCheckAsync(TEntity TEntity)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> PreUpdateCheckAsync(TEntity TEntity)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> PreDeleteCheckAsync(long Id)
        {
            return Task.FromResult(true);
        }

        public async virtual Task<bool> PreCreateApprovalCheckAsync(string TEntity)
        {
            var entity = JsonConvert.DeserializeObject<TEntity>(TEntity);
            return await PreCreateCheckAsync(entity);
        }

        public async virtual Task<bool> PreUpdateApprovalCheckAsync(string TEntity)
        {
            var entity = JsonConvert.DeserializeObject<TEntity>(TEntity);
            return await PreUpdateCheckAsync(entity);
        }

        public async virtual Task<bool> PreDeleteApprovalCheckAsync(string Id)
        {
            return await PreDeleteCheckAsync(long.Parse(Id));
        }
    }
}