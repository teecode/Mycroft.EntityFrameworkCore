using Mycroft.EntityFrameworkCore.Core.Models.Approval;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.IRepository.Approval
{
    public interface IApprovalRepository : ICRUD<Core.Models.Approval.Approval>
    {
        Task<bool> Approve(Core.Models.Approval.Approval approval);
        Task<bool> Decline(Core.Models.Approval.Approval approval);
    }
}
