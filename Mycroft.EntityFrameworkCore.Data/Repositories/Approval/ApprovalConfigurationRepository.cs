using Mycroft.EntityFrameworkCore.Data.IRepository.Approval;
using Mycroft.EntityFrameworkCore.Core.Models.Approval;
using Mycroft.EntityFrameworkCore.Data;

namespace Mycroft.EntityFrameworkCore.Data.Repository.Approval
{
    public class ApprovalConfigurationRepository : CRUD<ApprovalConfiguration>, IApprovalConfigurationRepository
    {
        public ApprovalConfigurationRepository(DataEngineDbContext dataEngineDbContext) : base(dataEngineDbContext)
        {
        }
    }
}