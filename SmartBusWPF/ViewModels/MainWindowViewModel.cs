using SmartBusWPF.Models.API;
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
            bool isStaySignedIn = Properties.Settings.Default.IsStaySignedIn;
            string driverID = Properties.Settings.Default.DriverID;
            string password = Properties.Settings.Default.Password;

            if (isStaySignedIn && !string.IsNullOrEmpty(driverID) && !string.IsNullOrEmpty(password))
            {
                driverID = cryptographyService.Decrypt(driverID);
                password = cryptographyService.Decrypt(password);

                LoginDriverDto loginDriverDto = new()
                {
                    DriverID = driverID,
                    Password = password,
                };

                HttpResponseModel<LoginDriverResponseDto> result = await httpClientService.PostAsync<LoginDriverDto, LoginDriverResponseDto>(loginDriverDto, APIConsts.Auth.LoginBusDriver);

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
                            ID = result.Response.ID,
                            FirstName = result.Response.FirstName,
                            LastName = result.Response.LastName,
                            Email = result.Response.Email,
                            DriverID = result.Response.DriverID,
                            PhoneNumber = result.Response.PhoneNumber,
                            Country = result.Response.Country
                        },
                        Bus = new BusModel()
                        {
                            ID = result.Response.BusDto.ID,
                            BusNumber = result.Response.BusDto.BusNumber,
                            CurrentLocation = result.Response.BusDto.CurrentLocation,
                            Capacity = result.Response.BusDto.Capacity
                        },
                        AuthToken = result.Response.Token,
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