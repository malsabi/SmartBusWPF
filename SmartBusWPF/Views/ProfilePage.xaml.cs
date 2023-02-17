using SmartBusWPF.ViewModels;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF.Views
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            DataContext = App.Current.Services.GetService<ProfileViewModel>();
            InitializeComponent();
        }
    }
}