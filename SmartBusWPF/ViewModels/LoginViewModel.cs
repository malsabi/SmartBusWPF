using SmartBusWPF.Models;
using SmartBusWPF.Common.Consts;
using CommunityToolkit.Mvvm.Input;
using SmartBusWPF.DTOs.Auth.Login;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;
        private readonly IHttpClientService httpClientService;
        private readonly ICryptographyService cryptographyService;
        public LoginViewModel(INavigationService navigationService,
                              IHttpClientService httpClientService,
                              ICryptographyService cryptographyService)
        {
            this.navigationService = navigationService;
            this.httpClientService = httpClientService;
            this.cryptographyService = cryptographyService;
            Initialize();
        }

        public IRelayCommand LoginCommand { get; private set; }

        public IRelayCommand LogsCommand { get; private set; }

        private string driverID;
        public string DriverID
        {
            get => driverID;
            set => SetProperty(ref driverID, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private bool isStaySignedIn;
        public bool IsStaySignedIn
        {
            get => isStaySignedIn;
            set => SetProperty(ref isStaySignedIn, value);
        }

        private string loginStatus;
        public string LoginStatus
        {
            get => loginStatus;
            set => SetProperty(ref loginStatus, value);
        }

        private bool showLoginStatus;
        public bool ShowLoginStatus
        {
            get => showLoginStatus;
            set => SetProperty(ref showLoginStatus, value);
        }

        private void Initialize()
        {
            LoginCommand = new RelayCommand(Login);
            LogsCommand = new RelayCommand(Logs);
        }

        private async void Login()
        {
            LoginDriverDto loginDriverDto = new()
            {
                DriverID = DriverID,
                Password = Password
            };

            HttpResponseModel<string> result = await httpClientService.PostAsync<LoginDriverDto, string>(loginDriverDto, APIConsts.Auth.LoginBusDriver);

            if (result != null && result.IsSuccess)
            {
                Properties.Settings.Default.IsStaySignedIn = IsStaySignedIn;

                if (IsStaySignedIn)
                {
                    Properties.Settings.Default.DriverID = cryptographyService.Encrypt(loginDriverDto.DriverID);
                    Properties.Settings.Default.Password = cryptographyService.Encrypt(loginDriverDto.Password);
                }

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
            else
            {
                if (result == null)
                {
                    LoginStatus = "Server is not responding";
                }
                else
                {
                    LoginStatus = result.ProblemDetails.Detail;
                }
                ShowLoginStatus = true;
            }
        }

        private void Logs()
        {
            navigationService.Navigate<LogsViewModel>();
        }
    }
}