using SmartBusWPF.ViewModels;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF.Views
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<LoginViewModel>();
        }
    }
}