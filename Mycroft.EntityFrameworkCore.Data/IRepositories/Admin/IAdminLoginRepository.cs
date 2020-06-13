using Mycroft.EntityFrameworkCore.Core.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.IRepository.Admin
{
    public interface IAdminLoginRepository:ICRUD<AdminLogin>
    {
        Task<Tuple<bool, AdminLogin>> AuthenticateAdmin(AdminLogin adminLogin);
    }
}
