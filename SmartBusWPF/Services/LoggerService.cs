using System;
using SmartBusWPF.Common.Enums;
using System.Collections.Generic;
using SmartBusWPF.Models.Logging;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    internal class LoggerService : ILoggerService
    {
        public ICollection<LogModel> Logs { get; private set; }

        public LoggerService()
        {
            Logs = new List<LogModel>();
        }

        public void Log(LogLevel logLevel, string source, string message)
        {
            Log(new LogModel()
            {
                LogLevel = logLevel,
                Source = source,
                Message = message,
                Timestamp = DateTime.Now
            });
        }

        public void Log(LogModel logModel)
        {
            Logs.Add(logModel);
        }
    }
}