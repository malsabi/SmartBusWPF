using System.Threading.Tasks;
using System.Collections.Generic;
using SmartBusWPF.DTOs.Notification;

namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface IBusNotificationService
    {
        public ICollection<BusNotificationDto> BusNotificationDtos { get; }
        public Task JoinBusGroupAsync(int busId);
        public Task LeaveBusGroupAsync(int busId);
    }
}