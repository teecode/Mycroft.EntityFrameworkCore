using Mycroft.EntityFrameworkCore.Core.Models.Admin;
using Mycroft.EntityFrameworkCore.Core.Models.Approval;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Cache
{
    public interface ICacheManager
    {
        Task<List<AdminRole>> AllRoles(bool reload = false);

        Task<AdminRole> Role(long Id);

        Task<AdminRole> Role(string code);

        Task<AdminRole> RoleByName(string name);

        Task<List<Action>> AllActions(bool reload = false);

        Task<Action> Actions(long Id);

        Task<Action> Actions(string actionName);

        Task<List<ApprovalConfiguration>> AllApprovalConfigurations(bool reload = false);

        Task<ApprovalConfiguration> ApprovalConfiguration(long Id);
    }
}