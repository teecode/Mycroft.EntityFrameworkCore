using System;

namespace Mycroft.EntityFrameworkCore.Core.Helpers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ApprovalActionAttribute : Attribute
    {
        public string ActionName { get; set; }

        public ApprovalActionAttribute(string actionName)
        {
            ActionName = actionName;
        }
    }
}