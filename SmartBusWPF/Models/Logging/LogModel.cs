using System;
using SmartBusWPF.Common.Enums;

namespace SmartBusWPF.Models.Logging
{
    public class LogModel
    {
        public DateTime Timestamp { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
    }
}