using System;
using System.Collections.Generic;
using System.Text;

namespace Mycroft.EntityFrameworkCore.Data.IRepository.Admin
{
    public interface IAdminRepository : ICRUD<Core.Models.Admin.Admin>, IApprovable<Core.Models.Admin.Admin>
    {
    }
}
