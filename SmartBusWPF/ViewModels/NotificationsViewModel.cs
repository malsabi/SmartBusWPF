using SmartBusWPF.Models;
using System.Collections.ObjectModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class NotificationsViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IBusNotificationService busNotificationService;

        public NotificationsViewModel(INavigationService navigationService,
                                      IBusNotificationService busNotificationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.busNotificationService = busNotificationService;
            Initialize();
        }

        public ObservableCollection<NotificationModel> Notifications { get; private set; }

        private async void Initialize()
        {
            await busNotificationService.JoinBusGroupAsync(1);
        }
    }
}