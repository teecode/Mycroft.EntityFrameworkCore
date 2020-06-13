using System;
using System.ComponentModel.DataAnnotations.Schema;
using static Mycroft.EntityFrameworkCore.Core.Models.Enumerators;

namespace Mycroft.EntityFrameworkCore.Core.Models.Approval
{
    public class Approval : BaseEntity
    {
        public long ApprovalConfigurationId { get; set; }
        [ForeignKey("ApprovalConfigurationId")]
        public ApprovalConfiguration ApprovalConfiguration { get; set; }
        public string Assembly { get; set; }
        public string ActionClass { get; set; }
        public string ActionMethod { get; set; }
        public string PreviousObject { get; set; }
        public string CurrentObject { get; set; }
        public long LoggedBy { get; set; }
        public long? ApprovedBy { get; set; }
        public USERTYPE CreatedByUserType { get; set; }
        public USERTYPE? ApprovedByUserType { get; set; }
        public DateTime? DateApproved { get; set; }
        public string Comment { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;
    }
}