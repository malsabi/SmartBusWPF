using SmartBusWPF.Models;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;

        public ProfileViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private string fullName;
        public string FullName
        {
            get => string.Format("{0} {1}", FirstName, LastName);
            set => SetProperty(ref fullName, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string driverID;
        public string DriverID
        {
            get => driverID;
            set => SetProperty(ref driverID, value);
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        private string country;
        public string Country
        {
            get => country;
            set => SetProperty(ref country, value);
        }

        private void Initialize()
        {
            BusDriverSessionModel busDriverSession = App.Current.BusDriverSession;
            if (busDriverSession != null)
            {
                FirstName = busDriverSession.BusDriver.FirstName;
                LastName = busDriverSession.BusDriver.LastName;
                FullName = string.Format("{0} {1}", FirstName, LastName);
                Email = busDriverSession.BusDriver.Email;
                DriverID = busDriverSession.BusDriver.DriverID;
                PhoneNumber = busDriverSession.BusDriver.PhoneNumber;
                Country = busDriverSession.BusDriver.Country;
            }
        }
    }
}