using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.IRepository
{
    public interface IUnitofWork
    {
        IDbContextTransaction BeginTransaction();

        Task<bool> SubmitChangesAsync();

        Task Refresh();
    }
}