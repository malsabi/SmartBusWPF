﻿using SmartBusWPF.Services;
using SmartBusWPF.ViewModels;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBusWPF
{
    public static class DepdencyInjection
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindowViewModel));
            services.AddTransient(typeof(LoginViewModel));
            services.AddTransient(typeof(HomeViewModel));
            services.AddTransient(typeof(TripViewModel));
            services.AddTransient(typeof(LogsViewModel));
            services.AddTransient(typeof(NotificationsViewModel));
            services.AddTransient(typeof(ProfileViewModel));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<IProcessMonitorService, ProcessMonitorService>();
            services.AddSingleton<ICryptographyService, CryptographyService>();
            services.AddSingleton<IServerSocketService, ServerSocketService>();
            services.AddSingleton<IClientSocketService, ClientSocketService>();
            services.AddSingleton<IHttpClientService, HttpClientService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IBusNotificationService>(new BusNotificationService(APIConsts.NotificationHubEndPoint));
            services.AddSingleton<IHuskyLensService, HuskyLensService>();
            return services;
        }
    }
}