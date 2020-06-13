using System;
using System.Collections.Generic;
using System.Text;

namespace Mycroft.EntityFrameworkCore.Core.Models
{
    [Serializable]
    public class BaseEntity
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}
