using System;
using System.Collections.Generic;
using System.Text;

namespace Mycroft.EntityFrameworkCore.Core.Models.Admin
{
    public class AdminRole:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

