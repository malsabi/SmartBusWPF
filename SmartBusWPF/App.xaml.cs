using System;
using System.Net;
using System.Windows;
using SmartBusWPF.Views;
using SmartBusWPF.Models;
using SmartBusWPF.Messages;
using SmartBusWPF.Commands;
using SmartBusWPF.ViewModels;
using SmartBusWPF.Common.Enums;
using CommunityToolkit.Mvvm.Messaging;
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

        #region "Properties"
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }
        #endregion

        #region "Private Methods"
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
            navigationService.RegisterView(typeof(HomeViewModel), new HomePage());
            navigationService.RegisterView(typeof(TripViewModel), new TripPage());
            navigationService.RegisterView(typeof(LogsViewModel), new LogsPage());
        }

        /// <summary>
        /// Represents an event when the application starts.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            IProcessMonitorService processMonitorService = Services.GetService<IProcessMonitorService>();
            processMonitorService.OnException += OnProcessMonitorExceptionHandler;

            if (processMonitorService.IsProcessRunning())
            {
                processMonitorService.TerminateProcess();
            }
            processMonitorService.StartProcess();

            IServerSocketService serverSocketService = Services.GetService<IServerSocketService>();
            serverSocketService.OnServerStartedListening += OnServerStartedListeningHandler;
            serverSocketService.OnServerStoppedListening += OnServerStoppedListeningHandler;
            serverSocketService.OnClientConnected += OnClientConnectedHandler;
            serverSocketService.OnClientMessageSent += OnClientMessageSentHandler;
            serverSocketService.OnClientMessageReceived += OnClientMessageReceivedHandler;
            serverSocketService.OnClientDisconnected += OnClientDisconnectedHandler;
            serverSocketService.OnClientException += OnClientExceptionHandler;
            serverSocketService.StartListening(1669);
            serverSocketService.AcceptClientAsync();

            MainWindow = new MainWindow();
            MainWindow.Show();

            base.OnStartup(e);
        }


        #endregion

        #region "Public Methods"
        public void Log(LogLevel logLevel, string source, string message)
        {
            Dispatcher.Invoke(delegate
            {
                WeakReferenceMessenger.Default.Send(new LogMessage(new LogModel()
                {
                    Timestamp = DateTime.Now,
                    LogLevel = logLevel,
                    Source = source,
                    Message = message
                }));
            });
        }
        #endregion

        #region "Event Handlers"
        private void OnProcessMonitorExceptionHandler(Exception ex)
        {
            Log(LogLevel.Error, "ProcessMonitor", ex.Message);
        }

        private void OnServerStartedListeningHandler(IPEndPoint endPoint)
        {
            Log(LogLevel.Info, "Server", $"Server started listening on {endPoint.Address}:{endPoint.Port}");
        }

        private void OnServerStoppedListeningHandler()
        {
            Log(LogLevel.Info, "Server", "Server stopped listening");
        }

        private async void OnClientConnectedHandler(IClientSocketService client)
        {
            Log(LogLevel.Info, "Client", $"Client [{client.EndPoint}] Connected Successfully");
            await client.SendAsync(AppCommands.OPEN_HUSKY_LENS_COMMAND, "SmartBus.HuskyLens");
            await client.SendAsync(AppCommands.START_WORKER_COMMAND, "SmartBus.HuskyLens");
        }

        private void OnClientMessageSentHandler(IClientSocketService client, string command, string content)
        {
            Log(LogLevel.Info, "Client", $"Client [{client.EndPoint}] Sent [Command: {command}, Content: {content}]");
        }
        
        private void OnClientMessageReceivedHandler(IClientSocketService client, string command, string content)
        {
            switch (command)
            {
                case AppCommands.LOG_MESSAGE_COMMAND:
                    Log(LogLevel.Info, "Python", content);
                    break;

                case AppCommands.DETECTED_IMAGE_COMMAND:
                    Log(LogLevel.Info, "Python", content);
                    break;
            }
        }

        private void OnClientDisconnectedHandler(IClientSocketService client)
        {
            Log(LogLevel.Warning, "Client", $"Client [{client.EndPoint}] Disconnected");
        }
        
        private void OnClientExceptionHandler(IClientSocketService client, Exception ex)
        {
            Log(LogLevel.Error, "Client", $"Client [{client.EndPoint}] Exception: [{ex.Message}]");
        }
        #endregion
    }
}