using System.Threading.Tasks;
using System.Collections.Generic;
using SmartBusWPF.DTOs.Notification;
using Microsoft.AspNetCore.SignalR.Client;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class BusNotificationService : IBusNotificationService
    {
        private HubConnection hubConnection;

        public BusNotificationService(string hubUrl)
        {
            Initialize(hubUrl);
        }

        public ICollection<BusNotificationDto> BusNotificationDtos { get; private set; }

        private void Initialize(string hubUrl)
        {
            BusNotificationDtos = new List<BusNotificationDto>();

            hubConnection = new HubConnectionBuilder()
               .WithUrl(hubUrl)
               .Build();

            hubConnection.On("ReceiveNotification", (BusNotificationDto busNotificationDto) =>
            {
                BusNotificationDtos.Add(busNotificationDto);
            });
        }

        public async Task JoinBusGroupAsync(int busId)
        {
            await hubConnection.StartAsync();
            await hubConnection.SendAsync("JoinBusGroup", busId);
        }

        public async Task LeaveBusGroupAsync(int busId)
        {
            await hubConnection.SendAsync("LeaveBusGroup", busId);
            await hubConnection.StopAsync();
        }
    }
}