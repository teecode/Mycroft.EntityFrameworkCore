using System.ComponentModel.DataAnnotations.Schema;

namespace Mycroft.EntityFrameworkCore.Core.Models.Approval
{
    public class ApprovalConfiguration : BaseEntity
    {
        public long ActionId { get; set; }

        [ForeignKey("ActionId")]
        public Action Action { get; set; }

        public long? ApprovingRoleId { get; set; }
        public long? ApprovingUserId { get; set; }
        public bool? AgentApproval { get; set; } = false;
    }
}