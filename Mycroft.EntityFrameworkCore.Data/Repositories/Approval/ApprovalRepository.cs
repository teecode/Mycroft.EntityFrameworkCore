using Mycroft.EntityFrameworkCore.Data.IRepository.Approval;
using Mycroft.EntityFrameworkCore.Core.Utility.Exceptions;
using Mycroft.EntityFrameworkCore.Data;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.Repository.Approval
{
    public class ApprovalRepository : CRUD<Core.Models.Approval.Approval>, IApprovalRepository
    {
        public ApprovalRepository(DataEngineDbContext dataEngineDbContext) : base(dataEngineDbContext)
        {
        }

        public async Task<bool> Approve(Core.Models.Approval.Approval approval)
        {
            try
            {
                if (approval.ApprovalConfiguration.Action.PreApprovalCheck.HasValue && approval.ApprovalConfiguration.Action.PreApprovalCheck.Value)
                {
                    var precheckApprovalEntry = await ApprovalPreCheck(approval);
                    if (!precheckApprovalEntry)
                        throw new NotVerifiedException("Approval Failed Precheck, Kindly decline and request regeneration.");
                }

                Assembly assembly = Assembly.GetAssembly(Type.GetType($"{approval.ApprovalConfiguration.Action.Assembly}.{approval.ApprovalConfiguration.Action.ActionClass}"));

                MethodInfo methodInfo = assembly.GetTypes().Where(x => x.Name.ToLower() == approval.ApprovalConfiguration.Action.ActionClass.ToLower())
                          .SelectMany(t => t.GetMethods()).SingleOrDefault(m => m.Name.ToLower() == approval.ApprovalConfiguration.Action.ActionMethod.ToLower());

                if (methodInfo != null)
                {
                    object result = null;
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object classInstance = Activator.CreateInstance(methodInfo.DeclaringType, new object[] { _context });

                    //Only public methods are invokable

                    //If the method has no parameter
                    if (parameters.Length == 0)
                    {
                        result = await Task.Run(() => methodInfo.Invoke(classInstance, null));
                    }
                    else
                    {
                        object[] parametersArray = new object[] { approval.CurrentObject };
                        result = await Task.Run(() => methodInfo.Invoke(classInstance, parametersArray));
                    }
                    approval.ApprovalStatus = Core.Models.Enumerators.ApprovalStatus.Approved;
                    await UpdateAsync(approval);
                    return true;
                }
                else
                    throw new Exception($"no method found with name {approval.ApprovalConfiguration.Action.ActionMethod}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> ApprovalPreCheck(Core.Models.Approval.Approval approval)
        {
            try
            {
                Assembly assembly = Assembly.GetAssembly(Type.GetType($"{approval.ApprovalConfiguration.Action.PreApprovalAssembly}.{approval.ApprovalConfiguration.Action.PreApprovalActionClass}"));

                MethodInfo methodInfo = assembly.GetTypes().Where(x => x.Name.ToLower() == approval.ApprovalConfiguration.Action.PreApprovalActionClass.ToLower())
                          .SelectMany(t => t.GetMethods()).SingleOrDefault(m => m.Name.ToLower() == approval.ApprovalConfiguration.Action.PreApprovalActionMethod.ToLower());

                if (methodInfo != null)
                {
                    object result = false;
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object classInstance = Activator.CreateInstance(methodInfo.DeclaringType, new object[] { _context });

                    if (parameters.Length == 0)
                    {
                        result = await Task.Run(() => methodInfo.Invoke(classInstance, null));
                    }
                    else
                    {
                        object[] parametersArray = new object[] { approval.CurrentObject };
                        result = await Task.Run(() => methodInfo.Invoke(classInstance, parametersArray));
                    }

                    var resulted = (Task<bool>)result;
                    var resultedbool = resulted.Result;
                    return resultedbool;
                }
                else
                    throw new Exception($"no method found with name {approval.ApprovalConfiguration.Action.PreApprovalActionMethod}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Decline(Core.Models.Approval.Approval approval)
        {
            try
            {
                approval.ApprovalStatus = Core.Models.Enumerators.ApprovalStatus.Approved;
                await UpdateAsync(approval);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}