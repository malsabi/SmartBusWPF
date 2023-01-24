using System.Windows;
using SmartBusWPF.ViewModels;
using SmartBusWPF.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Current.Services.GetService<INavigationService>().SetCurrentFrame(NavigationFrame);
            DataContext = App.Current.Services.GetService<MainWindowViewModel>();
        }
    }
}