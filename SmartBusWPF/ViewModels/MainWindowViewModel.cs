using SmartBusWPF.Models.API;
using SmartBusWPF.Models.Bus;
using SmartBusWPF.Common.Consts;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;
using SmartBusWPF.DTOs.Auth.Login.Response;
using SmartBusWPF.DTOs.Auth.Login.Request;

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

                LoginBusDriverRequestDto loginDriverDto = new()
                {
                    DriverID = driverID,
                    Password = password,
                };

                HttpResponseModel<LoginBusDriverResponseDto> result = await httpClientService.PostAsync<LoginBusDriverRequestDto, LoginBusDriverResponseDto>(loginDriverDto, APIConsts.Auth.LoginBusDriver);

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
                            ID = result.Response.BusDriverDto.ID,
                            FirstName = result.Response.BusDriverDto.FirstName,
                            LastName = result.Response.BusDriverDto.LastName,
                            Email = result.Response.BusDriverDto.Email,
                            DriverID = result.Response.BusDriverDto.DriverID,
                            PhoneNumber = result.Response.BusDriverDto.PhoneNumber,
                            Country = result.Response.BusDriverDto.Country
                        },
                        Bus = new BusModel()
                        {
                            ID = result.Response.BusDto.ID,
                            LicenseNumber = result.Response.BusDto.LicenseNumber,
                            CurrentLocation = result.Response.BusDto.CurrentLocation,
                            Capacity = result.Response.BusDto.Capacity
                        },
                        AuthToken = result.Response.AuthToken,
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