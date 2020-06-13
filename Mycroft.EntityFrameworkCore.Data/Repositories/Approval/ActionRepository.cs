using Mycroft.EntityFrameworkCore.Data.IRepository.Approval;
using Mycroft.EntityFrameworkCore.Core.Models.Approval;
using Mycroft.EntityFrameworkCore.Data;

namespace Mycroft.EntityFrameworkCore.Data.Repository.Approval
{
    public class ActionRepository : CRUD<Action>, IActionRepository
    {
        public ActionRepository(DataEngineDbContext dataEngineDbContext) : base(dataEngineDbContext)
        {
        }
    }
}