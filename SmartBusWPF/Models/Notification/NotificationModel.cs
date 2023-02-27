using System;

namespace SmartBusWPF.Models.Notification
{
    public class NotificationModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsOpened { get; set; }
    }
}