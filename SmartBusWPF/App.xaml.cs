using System;
using System.Windows;
using SmartBusWPF.Views;
using SmartBusWPF.ViewModels;
using SmartBusWPF.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF
{
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            ConfigureNavigation();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddServices();
            services.AddViewModels();
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Configures the navigation service.
        /// </summary>
        private void ConfigureNavigation()
        {
            INavigationService navigationService = Services.GetService<INavigationService>();
            navigationService.RegisterView(typeof(LoginViewModel), new LoginPage());
        }

        /// <summary>
        /// Represents an event when the application starts.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}