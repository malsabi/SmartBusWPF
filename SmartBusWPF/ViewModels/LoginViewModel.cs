using SmartBusWPF.Models.API;
using SmartBusWPF.Common.Consts;
using CommunityToolkit.Mvvm.Input;
using SmartBusWPF.DTOs.Auth.Login;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IHttpClientService httpClientService;
        private readonly ICryptographyService cryptographyService;

        public LoginViewModel(INavigationService navigationService,
                              IHttpClientService httpClientService,
                              ICryptographyService cryptographyService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.httpClientService = httpClientService;
            this.cryptographyService = cryptographyService;
            Initialize();
        }

        public IRelayCommand LoginCommand { get; private set; }

        private string driverID;
        public string DriverID
        {
            get => driverID;
            set
            {
                SetProperty(ref driverID, value);
                if (LoginCommand != null) 
                {
                    LoginCommand.NotifyCanExecuteChanged();
                }
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                if (LoginCommand != null)
                {
                    LoginCommand.NotifyCanExecuteChanged();
                }
            }
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

        private bool isLoginInProcess;
        public bool IsLoginInProcess
        {
            get => isLoginInProcess;
            set => SetProperty(ref isLoginInProcess, value);
        }

        private string loginButtonText;
        public string LoginButtonText
        {
            get => loginButtonText;
            set => SetProperty(ref loginButtonText, value);
        }

        private void Initialize()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
            IsStaySignedIn = false;
            ShowLoginStatus = false;
            IsLoginInProcess = false;
            LoginButtonText = "LOGIN";
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(DriverID) && !string.IsNullOrEmpty(Password) && !IsLoginInProcess;
        }

        private async void Login()
        {
            LoginButtonText = "LOGGING IN..";
            IsLoginInProcess = true;
            LoginCommand.NotifyCanExecuteChanged();
          
            LoginDriverDto loginDriverDto = new()
            {
                DriverID = DriverID,
                Password = Password
            };

            HttpResponseModel<LoginDriverResponseDto> result = await httpClientService.PostAsync<LoginDriverDto, LoginDriverResponseDto>(loginDriverDto, APIConsts.Auth.LoginBusDriver);

            LoginButtonText = "LOGIN";
            IsLoginInProcess = false;
            LoginCommand.NotifyCanExecuteChanged();

            if (result != null && result.IsSuccess)
            {
                Properties.Settings.Default.IsStaySignedIn = IsStaySignedIn;

                if (IsStaySignedIn)
                {
                    Properties.Settings.Default.DriverID = cryptographyService.Encrypt(loginDriverDto.DriverID);
                    Properties.Settings.Default.Password = cryptographyService.Encrypt(loginDriverDto.Password);
                }
                Properties.Settings.Default.Save();

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
    }
}