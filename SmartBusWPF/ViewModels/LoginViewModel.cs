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
        }
        


    }
}