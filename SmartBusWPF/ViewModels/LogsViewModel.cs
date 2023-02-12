using SmartBusWPF.Models;
using SmartBusWPF.Messages;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class LogsViewModel : ObservableObject, IRecipient<LogMessage>
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
            GoBackCommand = new RelayCommand(GoBack);
            WeakReferenceMessenger.Default.Register<LogMessage>(this);
        }

        private void GoBack()
        {
            navigationService.GoBack();
        }

        public void Receive(LogMessage message)
        {
            Logs.Add(message.Value);
        }
    }
}