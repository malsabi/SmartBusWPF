using SmartBusWPF.ViewModels;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF.Views
{
    public partial class TripPage : Page
    {
        public TripPage()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<TripViewModel>();
        }
    }
}