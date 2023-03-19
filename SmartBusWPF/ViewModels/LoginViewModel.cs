using MapsterMapper;
using SmartBusWPF.Models.API;
using SmartBusWPF.Models.Bus;
using CommunityToolkit.Mvvm.Input;
using SmartBusWPF.DTOs.Auth.Login.Response;
using SmartBusWPF.Common.Interfaces.Services;
using SmartBusWPF.Common.Interfaces.Services.API;

namespace SmartBusWPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly INavigationService navigationService;
        private readonly ICryptographyService cryptographyService;

        public LoginViewModel(IMapper mapper,
                              IAuthService authService,
                              INavigationService navigationService,
                              ICryptographyService cryptographyService) : base(navigationService)
        {
            this.mapper = mapper;
            this.authService = authService;
            this.navigationService = navigationService;
            this.cryptographyService = cryptographyService;
            Initialize();
        }

        #region "Properties"
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
        #endregion

        private void Initialize()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
            IsStaySignedIn = false;
            LoginStatus = "Server is not responding";
            ShowLoginStatus = false;
            IsLoginInProcess = false;
            LoginButtonText = "LOGIN";
        }

        #region "Login"
        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(DriverID) && !string.IsNullOrEmpty(Password) && !IsLoginInProcess;
        }

        private async void Login()
        {
            LoginButtonText = "LOGGING IN..";
            IsLoginInProcess = true;
            LoginCommand.NotifyCanExecuteChanged();

            HttpResponseModel<LoginBusDriverResponseDto> result = await authService.Login(DriverID, Password);

            LoginButtonText = "LOGIN";
            IsLoginInProcess = false;
            LoginCommand.NotifyCanExecuteChanged();

            if (result != null && result.IsSuccess)
            {
                Properties.Settings.Default.IsStaySignedIn = IsStaySignedIn;

                if (IsStaySignedIn)
                {
                    Properties.Settings.Default.DriverID = cryptographyService.Encrypt(DriverID);
                    Properties.Settings.Default.Password = cryptographyService.Encrypt(Password);
                }
                Properties.Settings.Default.Save();
                App.Current.BusDriverSession = new BusDriverSessionModel()
                {
                    BusDriver = mapper.Map<BusDriverModel>(result.Response.BusDriverDto),
                    Bus = mapper.Map<BusModel>(result.Response.BusDto),
                    AuthToken = result.Response.AuthToken,
                    IsActive = true
                };
                navigationService.Navigate<HomeViewModel>();
            }
            else
            {
                LoginStatus = result.ProblemDetails.Detail;
                ShowLoginStatus = true;
            }
        }
        #endregion
    }
}