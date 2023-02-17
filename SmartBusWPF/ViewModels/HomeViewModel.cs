using CommunityToolkit.Mvvm.Input;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;

        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }

        public IRelayCommand ProfileCommand { get; private set; }
        public IRelayCommand StartTripCommand { get; private set; }

        private void Initialize()
        {
            ProfileCommand = new RelayCommand(Profile);
            StartTripCommand = new RelayCommand(StartTrip);
        }

        private void Profile()
        {
            //navigationService.Navigate<LoginViewModel>();
        }

        private void StartTrip()
        {
            navigationService.Navigate<TripViewModel>();
        }
    }
}