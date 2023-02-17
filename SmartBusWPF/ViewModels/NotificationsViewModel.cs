using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class NotificationsViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;

        public NotificationsViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}