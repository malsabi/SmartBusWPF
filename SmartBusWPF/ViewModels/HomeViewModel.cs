using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;

        public HomeViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }

        public IRelayCommand ProfileCommand { get; private set; }
        public IRelayCommand StartTripCommand { get; private set; }
        public IRelayCommand LogsCommand { get; private set; }

        private void Initialize()
        {
            ProfileCommand = new RelayCommand(Profile);
            StartTripCommand = new RelayCommand(StartTrip);
            LogsCommand = new RelayCommand(Logs);
        }

        private void Profile()
        {
            navigationService.Navigate<LoginViewModel>();
        }

        private void StartTrip()
        {
            navigationService.Navigate<TripViewModel>();
        }

        private void Logs()
        {
            navigationService.Navigate<LogsViewModel>();
        }
    }
}