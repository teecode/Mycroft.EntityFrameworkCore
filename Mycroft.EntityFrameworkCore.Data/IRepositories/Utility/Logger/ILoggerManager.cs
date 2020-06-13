using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Logger
{
    public interface ILoggerManager
    {
        Task LogInfo(string message, string TrackingKey = "");
        Task LogWarning(string message, string TrackingKey = "");
        Task LogError(string message, string TrackingKey = "");
        Task LogException(Exception exception, string TrackingKey = "");
    }
}
