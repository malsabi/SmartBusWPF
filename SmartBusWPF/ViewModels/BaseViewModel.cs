using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }

        public IRelayCommand OpenHomeCommand { get; private set; }
        public IRelayCommand OpenProfileCommand { get; private set; }
        public IRelayCommand LogoutCommand { get; private set; }
        public IRelayCommand OpenLogsCommand { get; private set; }
        public IRelayCommand OpenNotificationsCommand { get; private set; }

        private void Initialize()
        {
            OpenHomeCommand = new RelayCommand(Home);   
            OpenProfileCommand = new RelayCommand(Profile);
            LogoutCommand = new RelayCommand(Logout);
            OpenLogsCommand = new RelayCommand(Logs);
            OpenNotificationsCommand = new RelayCommand(Notifications);
        }

        private void Home()
        {
            navigationService.Navigate<HomeViewModel>();
        }

        private void Profile()
        {
            navigationService.Navigate<ProfileViewModel>();
        }

        private void Logout()
        {
            Properties.Settings.Default.IsStaySignedIn = false;
            Properties.Settings.Default.DriverID = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();

            App.Current.BusDriverSession = null;

            LoginViewModel loginViewModel = App.Current.Services.GetService<LoginViewModel>();

            loginViewModel.DriverID = "";
            loginViewModel.Password = "";
            loginViewModel.IsStaySignedIn = false;
            loginViewModel.LoginStatus = "";
            loginViewModel.ShowLoginStatus = false;

            navigationService.Navigate<LoginViewModel>();
        }

        private void Logs()
        {
            navigationService.Navigate<LogsViewModel>();
        }

        private void Notifications()
        {
            navigationService.Navigate<NotificationsViewModel>();
        }
    }
}