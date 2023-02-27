using SmartBusWPF.Models.API;
using SmartBusWPF.Common.Enums;
using CommunityToolkit.Mvvm.Input;
using SmartBusWPF.Common.Interfaces.Services;
using SmartBusWPF.Common.Interfaces.Services.API;

namespace SmartBusWPF.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly ITripService tripService;
        private readonly INavigationService navigationService;
        
        public HomeViewModel(ITripService tripService, INavigationService navigationService) : base(navigationService)
        {
            this.tripService = tripService;
            this.navigationService = navigationService;
            Initialize();
        }

        private bool showDestinationModal;
        public bool ShowDestinationModal
        {
            get => showDestinationModal;
            set => SetProperty(ref showDestinationModal, value);
        }

        public IRelayCommand ToggleModalCommand { get; private set; }

        public IRelayCommand StartTripCommand { get; private set; }

        private void Initialize()
        {
            ToggleModalCommand = new RelayCommand(ToggleModal);
            StartTripCommand = new RelayCommand<DestinationType>(StartTrip);
        }

        private void ToggleModal()
        {
            ShowDestinationModal = !ShowDestinationModal;
        }

        private async void StartTrip(DestinationType destinationType)
        {
            int busID = App.Current.BusDriverSession.Bus.ID;
            string authToken = App.Current.BusDriverSession.AuthToken;

            HttpResponseModel<string> result = await tripService.StartBusTrip(busID, destinationType, authToken);

            ShowDestinationModal = false;

            if (result.IsSuccess)
            {
                App.Current.BusDriverSession.IsTripStarted = true;
                navigationService.Navigate<TripViewModel>();
            }
        }
    }
}