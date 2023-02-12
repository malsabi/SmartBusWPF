using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly INavigationService navigationService; 
        
        public LoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }
        
        public IRelayCommand LoginCommand { get; private set; }
        public IRelayCommand LogsCommand { get; private set; }

        private void Initialize()
        {
            LoginCommand = new RelayCommand(Login);
            LogsCommand = new RelayCommand(Logs);
        }

        private void Login()
        {
            navigationService.Navigate<HomeViewModel>();
        }

        private void Logs()
        {
            navigationService.Navigate<LogsViewModel>();
        }
    }
}