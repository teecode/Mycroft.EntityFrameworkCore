using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mycroft.EntityFrameworkCore.Core.Models.Admin
{
    public class AdminLogin : BaseEntity
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool IsActive { get; set; }
        public long AdminId { get; set; }

        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }

        public long AdminRoleId { get; set; }

        [ForeignKey("AdminRoleId")]
        public AdminRole AdminRole { get; set; }

        public bool ForceChangeofPassword { get; set; } = true;
        public DateTime DateLastChangedPassword { get; set; }
        public DateTime DateForNextPasswordChange { get; set; }
    }
}