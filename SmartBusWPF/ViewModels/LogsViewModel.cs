using SmartBusWPF.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class LogsViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;

        public ObservableCollection<LogModel> Logs { get; private set; }

        public IRelayCommand GoBackCommand { get; private set; }

        public LogsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }

        private void Initialize()
        {
            Logs = new ObservableCollection<LogModel>();
            foreach (LogModel log in App.Current.LoggerService.Logs)
            {
                Logs.Add(log);
            }
            GoBackCommand = new RelayCommand(GoBack);
        }

        private void GoBack()
        {
            navigationService.GoBack();
        }
    }
}