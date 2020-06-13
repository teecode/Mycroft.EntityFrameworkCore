using Mycroft.EntityFrameworkCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mycroft.EntityFrameworkCore.Core
{
    public class AuditLog :BaseEntity
    {
        public string TableName { get; set; }
        public string TablePk { get; set; }
        public string Title { get; set; }
        public string AuditAction { get; set; }
        public string AuditUser { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditData { get; set; }
        public string EntityType { get; set; }
    }
}
