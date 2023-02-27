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
            OpenHomeCommand = new RelayCommand(Home, CanNavigate);   
            OpenProfileCommand = new RelayCommand(Profile, CanNavigate);
            LogoutCommand = new RelayCommand(Logout, CanNavigate);
            OpenLogsCommand = new RelayCommand(Logs, CanNavigate);
            OpenNotificationsCommand = new RelayCommand(Notifications, CanNavigate);
        }

        private void Home()
        {
            navigationService.Navigate<HomeViewModel>();
        }

        private void Profile()
        {
            navigationService.Navigate<ProfileViewModel>();
        }

        private bool CanNavigate()
        {
            if (App.Current.BusDriverSession == null)
            {
                return true;
            }
            return App.Current.BusDriverSession != null && !App.Current.BusDriverSession.IsTripStarted;
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