using SmartBusWPF.Models;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.DTOs.Auth.Login;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;
        private readonly IHttpClientService httpClientService;
        private readonly ICryptographyService cryptographyService;

        public MainWindowViewModel(INavigationService navigationService,
                                   IHttpClientService httpClientService,   
                                   ICryptographyService cryptographyService)
        {
            this.navigationService = navigationService;
            this.httpClientService = httpClientService;
            this.cryptographyService = cryptographyService;
            Authenticate();
        }

        private async void Authenticate()
        {
            string driverID = Properties.Settings.Default.DriverID;
            string password = Properties.Settings.Default.Password;

            if (!string.IsNullOrEmpty(driverID) && !string.IsNullOrEmpty(password))
            {
                driverID = cryptographyService.Decrypt(driverID);
                password = cryptographyService.Decrypt(password);

                LoginDriverDto loginDriverDto = new LoginDriverDto()
                {
                    DriverID = driverID,
                    Password = password,
                };

                HttpResponseModel<string> result = await httpClientService.PostAsync<LoginDriverDto, string>(loginDriverDto, APIConsts.Auth.LoginBusDriver);

                if (result == null || !result.IsSuccess)
                {
                    navigationService.Navigate<LoginViewModel>();
                }
                else
                {
                    App.Current.BusDriverSession = new BusDriverSessionModel()
                    {
                        BusDriver = new BusDriverModel()
                        {
                        },
                        AuthToken = result.Response,
                        IsActive = true
                    };
                    navigationService.Navigate<HomeViewModel>();
                }
            }
            else
            {
                navigationService.Navigate<LoginViewModel>();
            }
        }
    }
}