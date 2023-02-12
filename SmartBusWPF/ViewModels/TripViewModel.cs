using SmartBusWPF.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class TripViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;

        private int totalStudents;
        public int TotalStudents
        {
            get => totalStudents;
            set => SetProperty(ref totalStudents, value);
        }

        public ObservableCollection<StudentModel> Students { get; private set; }

        public IRelayCommand ProfileCommand { get; private set; }

        public IRelayCommand StartTripCommand { get; private set; }

        public IRelayCommand LogsCommand { get; private set; }

        public TripViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }

        private void Initialize()
        {
            Students = new ObservableCollection<StudentModel>();
            ProfileCommand = new RelayCommand(Profile);
            StartTripCommand = new RelayCommand(StartTrip);
            LogsCommand = new RelayCommand(Logs);
        }

        private void Profile()
        {
            navigationService.Navigate<LoginViewModel>();
        }

        private void StartTrip()
        {
            navigationService.Navigate<TripViewModel>();
        }

        private void Logs()
        {
            navigationService.Navigate<LogsViewModel>();
        }
    }
}