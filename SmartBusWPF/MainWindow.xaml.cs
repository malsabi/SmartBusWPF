using System.Windows;

namespace SmartBusWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //App.Current.Services.GetService<INavigationService>().SetCurrentFrame(NavigationFrame);
            //DataContext = App.Current.Services.GetService<MainWindowViewModel>();
        }
    }
}