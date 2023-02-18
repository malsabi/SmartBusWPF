using System;

namespace SmartBusWPF.Models
{
    public class NotificationModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}