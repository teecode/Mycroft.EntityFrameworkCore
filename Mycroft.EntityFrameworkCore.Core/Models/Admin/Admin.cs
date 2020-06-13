using System;
using System.Collections.Generic;
using System.Text;

namespace Mycroft.EntityFrameworkCore.Core.Models.Admin
{
    public class Admin : BaseInformation
    {
        public long AdminloginId { get; set; }
        public AdminLogin Adminlogin { get; set; }
    }
}
