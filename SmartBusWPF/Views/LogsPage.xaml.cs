using SmartBusWPF.ViewModels;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF.Views
{
    public partial class LogsPage : Page
    {
        public LogsPage()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<LogsViewModel>();
        }
    }
}