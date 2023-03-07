using System.Collections.ObjectModel;
using SmartBusWPF.Models.Notification;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class NotificationsViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;

        public NotificationsViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }

        public ObservableCollection<NotificationModel> Notifications { get; private set; }

        private void Initialize()
        {
        }
    }
}