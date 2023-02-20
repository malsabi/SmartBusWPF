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

        public IRelayCommand StartTripCommand { get; private set; }

        private void Initialize()
        {
            StartTripCommand = new RelayCommand(StartTrip);
        }

        private void StartTrip()
        {
            App.Current.BusDriverSession.IsTripStarted = true;
            navigationService.Navigate<TripViewModel>();
        }
    }
}