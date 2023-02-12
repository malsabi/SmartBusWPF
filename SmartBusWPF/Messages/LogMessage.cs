using SmartBusWPF.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace SmartBusWPF.Messages
{
    public class LogMessage : ValueChangedMessage<LogModel>
    {
        public LogMessage(LogModel logEntry) : base(logEntry)
        {
        }
    }
}