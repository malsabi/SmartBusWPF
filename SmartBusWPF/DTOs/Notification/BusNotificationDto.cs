using System;

namespace SmartBusWPF.DTOs.Notification
{
    public class BusNotificationDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}