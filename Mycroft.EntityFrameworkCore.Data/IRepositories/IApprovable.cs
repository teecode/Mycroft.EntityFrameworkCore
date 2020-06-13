using Mycroft.EntityFrameworkCore.Core.Models;
using System;
using System.Threading.Tasks;
using static Mycroft.EntityFrameworkCore.Core.Models.Enumerators;

namespace Mycroft.EntityFrameworkCore.Data.IRepository
{
    public interface IApprovable<TEntity> where TEntity : BaseEntity
    {
        Task<Tuple<TEntity, ApprovalStatus, string>> CreateApprovableAsync(TEntity TEntity);

        Task<bool> PreCreateCheckAsync(TEntity TEntity);

        Task<bool> PreCreateApprovalCheckAsync(string TEntity);

        Task<Tuple<ApprovalStatus, string>> UpdateApprovableAsync(TEntity TEntity);

        Task<bool> PreUpdateCheckAsync(TEntity TEntity);

        Task<bool> PreUpdateApprovalCheckAsync(string TEntity);

        Task<Tuple<bool, ApprovalStatus, string>> DeleteApprovableAsync(long Id);

        Task<bool> PreDeleteCheckAsync(long Id);

        Task<bool> PreDeleteApprovalCheckAsync(string Id);
    }
}