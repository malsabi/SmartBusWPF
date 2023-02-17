using SmartBusWPF.Models;
using SmartBusWPF.Common.Enums;
using System.Collections.Generic;

namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface ILoggerService
    {
        public ICollection<LogModel> Logs { get; }
        public void Log(LogLevel logLevel, string source, string message);
        public void Log(LogModel logModel);
    }
}