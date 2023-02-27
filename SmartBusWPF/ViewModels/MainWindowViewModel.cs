using System;
using MapsterMapper;
using System.Net.Http;
using System.Threading;
using SmartBusWPF.Models.API;
using SmartBusWPF.Models.Bus;
using System.Threading.Tasks;
using SmartBusWPF.Common.Consts;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.DTOs.Auth.Login.Response;
using SmartBusWPF.Common.Interfaces.Services;
using SmartBusWPF.Common.Interfaces.Services.API;

namespace SmartBusWPF.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly INavigationService navigationService;
        private readonly ICryptographyService cryptographyService;

        public MainWindowViewModel(IMapper mapper,
                                   IAuthService authService,
                                   INavigationService navigationService,
                                   ICryptographyService cryptographyService)
        {
            this.mapper = mapper;
            this.authService = authService;
            this.navigationService = navigationService;
            this.cryptographyService = cryptographyService;
            Initialize();
         
        }

        private bool isServiceAvailable;
        public bool IsServiceAvailable
        {
            get => !isServiceAvailable;
            set => SetProperty(ref isServiceAvailable, value);
        }


        private void Initialize()
        {
            IsServiceAvailable = true;
            CheckApiAvailabilityAsync();
        }

        private async void CheckApiAvailabilityAsync()
        {
            SemaphoreSlim apiCheckSemaphore = new(1, 1);
            while (true)
            {
                await apiCheckSemaphore.WaitAsync();
                if (!await IsAPIAvailableAsync())
                {
                    App.Current.IsServiceAvailable = false;
                    IsServiceAvailable = false;
                }
                else
                {
                    IsServiceAvailable = true;
                    Authenticate();
                }
                apiCheckSemaphore.Release();
                await Task.Delay(3000);
            }
        }

        private static async Task<bool> IsAPIAvailableAsync()
        {
            using (HttpClient httpClient = new())
            {
                try
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(5);
                    HttpResponseMessage response = await httpClient.GetAsync(string.Format("{0}{1}", APIConsts.LocalAPIEndPoint, APIConsts.Health.GetHealth));
                    return response.IsSuccessStatusCode;
                }
                catch
                {
                    return false;
                }
            }
        }

        private async void Authenticate()
        {
            bool isStaySignedIn = Properties.Settings.Default.IsStaySignedIn;
            string driverID = Properties.Settings.Default.DriverID;
            string password = Properties.Settings.Default.Password;

            if (App.Current.BusDriverSession != null && App.Current.BusDriverSession.IsActive)
            {
                return;
            }

            if (isStaySignedIn && !string.IsNullOrEmpty(driverID) && !string.IsNullOrEmpty(password))
            {
                driverID = cryptographyService.Decrypt(driverID);
                password = cryptographyService.Decrypt(password);

                HttpResponseModel<LoginBusDriverResponseDto> result = await authService.Login(driverID, password);

                if (result == null || !result.IsSuccess)
                {
                    navigationService.Navigate<LoginViewModel>();
                    return;
                }

                App.Current.BusDriverSession = new BusDriverSessionModel()
                {
                    BusDriver = mapper.Map<BusDriverModel>(result.Response.BusDriverDto),
                    Bus = mapper.Map<BusModel>(result.Response.BusDto),
                    AuthToken = result.Response.AuthToken,
                    IsActive = true
                };

                navigationService.Navigate<HomeViewModel>();
                return;
            }
            navigationService.Navigate<LoginViewModel>();
        }
    }
}