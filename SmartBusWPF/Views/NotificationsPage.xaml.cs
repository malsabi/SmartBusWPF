using SmartBusWPF.ViewModels;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF.Views
{
    public partial class NotificationsPage : Page
    {
        public NotificationsPage()
        {
            DataContext = App.Current.Services.GetService<NotificationsViewModel>();
            InitializeComponent();
        }
    }
}