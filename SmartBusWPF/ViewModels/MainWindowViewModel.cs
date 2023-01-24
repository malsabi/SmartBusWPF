using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;
        
        public MainWindowViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            navigationService.Navigate<LoginViewModel>();
        }
    }
}