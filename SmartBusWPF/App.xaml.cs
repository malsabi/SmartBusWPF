using System;
using System.Net;
using System.Windows;
using SmartBusWPF.Views;
using SmartBusWPF.Models;
using SmartBusWPF.Commands;
using SmartBusWPF.ViewModels;
using System.Windows.Controls;
using SmartBusWPF.Common.Enums;
using SmartBusWPF.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF
{
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        #region "Properties"
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Gets or sets the <see cref="BusDriverSessionModel"/> instance.
        /// </summary>
        public BusDriverSessionModel BusDriverSession { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LoggerService"/> instance.
        /// </summary>
        public ILoggerService LoggerService { get; private set; }
        #endregion

        #region "Private Methods"
        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new();
            {
                services.AddServices();
                services.AddViewModels();
            }
         
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            {
                LoggerService = serviceProvider.GetService<ILoggerService>();

                INavigationService navigationService = serviceProvider.GetService<INavigationService>();
                navigationService.RegisterView(typeof(LoginViewModel), typeof(LoginPage));
                navigationService.RegisterView(typeof(HomeViewModel), typeof(HomePage));
                navigationService.RegisterView(typeof(TripViewModel), typeof(TripPage));
                navigationService.RegisterView(typeof(LogsViewModel), typeof(LogsPage));
                navigationService.RegisterView(typeof(NotificationsViewModel), typeof(NotificationsPage));
                navigationService.RegisterView(typeof(ProfileViewModel), typeof(ProfilePage));

                IProcessMonitorService processMonitorService = serviceProvider.GetService<IProcessMonitorService>();
                processMonitorService.OnException += OnProcessMonitorExceptionHandler;

                if (processMonitorService.IsProcessRunning())
                {
                    processMonitorService.TerminateProcess();
                }
                processMonitorService.StartProcess();

                IServerSocketService serverSocketService = serviceProvider.GetService<IServerSocketService>();
                serverSocketService.OnServerStartedListening += OnServerStartedListeningHandler;
                serverSocketService.OnServerStoppedListening += OnServerStoppedListeningHandler;
                serverSocketService.OnClientConnected += OnClientConnectedHandler;
                serverSocketService.OnClientMessageSent += OnClientMessageSentHandler;
                serverSocketService.OnClientMessageReceived += OnClientMessageReceivedHandler;
                serverSocketService.OnClientDisconnected += OnClientDisconnectedHandler;
                serverSocketService.OnClientException += OnClientExceptionHandler;
                serverSocketService.StartListening(1669);
                serverSocketService.AcceptClientAsync();
            }
            return serviceProvider;
        }

        /// <summary>
        /// Represents an event when the application starts.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            Frame navigationFrame = new()
            {
                NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden
            };

            INavigationService navigationService = Services.GetService<INavigationService>();
            navigationService.SetCurrentFrame(navigationFrame);

            MainWindow = new MainWindow()
            {
                DataContext = Services.GetService<MainWindowViewModel>(),
                Content = navigationFrame
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Represents an event when the application exists.
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            IServerSocketService serverSocketService = Services.GetService<IServerSocketService>();
            serverSocketService.OnServerStartedListening -= OnServerStartedListeningHandler;
            serverSocketService.OnServerStoppedListening -= OnServerStoppedListeningHandler;
            serverSocketService.OnClientConnected -= OnClientConnectedHandler;
            serverSocketService.OnClientMessageSent -= OnClientMessageSentHandler;
            serverSocketService.OnClientMessageReceived -= OnClientMessageReceivedHandler;
            serverSocketService.OnClientDisconnected -= OnClientDisconnectedHandler;
            serverSocketService.OnClientException -= OnClientExceptionHandler;
            serverSocketService.StopListening();

            IProcessMonitorService processMonitorService = Services.GetService<IProcessMonitorService>();
            processMonitorService.OnException -= OnProcessMonitorExceptionHandler;

            if (processMonitorService.IsProcessRunning())
            {
                processMonitorService.TerminateProcess();
            }
            base.OnExit(e);
        }
        #endregion

        #region "Event Handlers"
        private void OnProcessMonitorExceptionHandler(Exception ex)
        {
            LoggerService.Log(LogLevel.Error, "ProcessMonitor", ex.Message);
        }

        private void OnServerStartedListeningHandler(IPEndPoint endPoint)
        {
            LoggerService.Log(LogLevel.Info, "Server", $"Server started listening on {endPoint.Address}:{endPoint.Port}");
        }

        private void OnServerStoppedListeningHandler()
        {
            LoggerService.Log(LogLevel.Info, "Server", "Server stopped listening");
        }

        private async void OnClientConnectedHandler(IClientSocketService client)
        {
            LoggerService.Log(LogLevel.Info, "Client", $"Client [{client.EndPoint}] Connected Successfully");
            await client.SendAsync(AppCommands.OPEN_HUSKY_LENS_COMMAND, "SmartBus.HuskyLens");
            await client.SendAsync(AppCommands.START_WORKER_COMMAND, "SmartBus.HuskyLens");
        }

        private void OnClientMessageSentHandler(IClientSocketService client, string command, string content)
        {
            LoggerService.Log(LogLevel.Info, "Client", $"Client [{client.EndPoint}] Sent [Command: {command}, Content: {content}]");
        }
        
        private void OnClientMessageReceivedHandler(IClientSocketService client, string command, string content)
        {
            switch (command)
            {
                case AppCommands.LOG_MESSAGE_COMMAND:
                    LoggerService.Log(LogLevel.Info, "Python", content);
                    break;

                case AppCommands.DETECTED_IMAGE_COMMAND:
                    LoggerService.Log(LogLevel.Info, "Python", content);
                    break;
            }
        }

        private void OnClientDisconnectedHandler(IClientSocketService client)
        {
            LoggerService.Log(LogLevel.Warning, "Client", $"Client [{client.EndPoint}] Disconnected");
        }
        
        private void OnClientExceptionHandler(IClientSocketService client, Exception ex)
        {
            LoggerService.Log(LogLevel.Error, "Client", $"Client [{client.EndPoint}] Exception: [{ex.Message}]");
        }
        #endregion
    }
}