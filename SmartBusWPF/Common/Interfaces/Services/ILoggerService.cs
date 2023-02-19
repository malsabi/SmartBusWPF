using SmartBusWPF.Common.Enums;
using System.Collections.Generic;
using SmartBusWPF.Models.Logging;

namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface ILoggerService
    {
        public ICollection<LogModel> Logs { get; }
        public void Log(LogLevel logLevel, string source, string message);
        public void Log(LogModel logModel);
    }
}