using SmartBusWPF.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class TripViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;

        private int totalStudents;
        public int TotalStudents
        {
            get => totalStudents;
            set => SetProperty(ref totalStudents, value);
        }

        public ObservableCollection<StudentModel> Students { get; private set; }

        public IRelayCommand StartTripCommand { get; private set; }


        public TripViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }

        private void Initialize()
        {
            Students = new ObservableCollection<StudentModel>();
            StartTripCommand = new RelayCommand(StartTrip);
        }

        private void StartTrip()
        {
            navigationService.Navigate<TripViewModel>();
        }
    }
}