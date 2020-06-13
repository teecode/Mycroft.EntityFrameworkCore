using static Mycroft.EntityFrameworkCore.Core.Models.Enumerators;

namespace Mycroft.EntityFrameworkCore.Core.Models.Approval
{
    public class Action : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Assembly { get; set; }
        public string ActionClass { get; set; }
        public string ActionMethod { get; set; }
        public bool? PreApprovalCheck { get; set; } = true;
        public string PreApprovalAssembly { get; set; }
        public string PreApprovalActionClass { get; set; }
        public string PreApprovalActionMethod { get; set; }
        public ActionMethodType ActionMethodType { get; set; } = ActionMethodType.Update;
    }
}