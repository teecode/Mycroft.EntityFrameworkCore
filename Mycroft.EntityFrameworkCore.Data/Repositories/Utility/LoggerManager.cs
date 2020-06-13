using Mycroft.EntityFrameworkCore.Data.IRepository.Utility.Logger;
using NLog;
using System;
using System.Threading.Tasks;

//using Microsoft.Extensions.Logging;

namespace Mycroft.EntityFrameworkCore.Data.Repository.Utility
{
    public class LoggerManager : ILoggerManager
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public async Task LogError(string message, string TrackingKey = "")
        {
            await Task.Run(() => _logger.Log(LogLevel.Error, $"{TrackingKey}||{message}"));
        }

        public async Task LogException(Exception exception, string TrackingKey = "")
        {
            await Task.Run(() => _logger.Log(LogLevel.Error, $"{exception?.InnerException?.Message}||{exception?.Message}||{exception.StackTrace}", exception, null));
        }

        public async Task LogInfo(string message, string TrackingKey = "")
        {
            await Task.Run(() => _logger.Log(LogLevel.Info, $"{TrackingKey}||{message}"));
        }

        public async Task LogWarning(string message, string TrackingKey = "")
        {
            await Task.Run(() => _logger.Log(LogLevel.Warn, $"{TrackingKey}||{message}"));
        }
    }
}