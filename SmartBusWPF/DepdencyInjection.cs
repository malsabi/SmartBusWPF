using SmartBusWPF.Services;
using SmartBusWPF.ViewModels;
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
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IProcessMonitorService, ProcessMonitorService>();
            services.AddSingleton<IServerSocketService, ServerSocketService>();
            services.AddSingleton<IClientSocketService, ClientSocketService>();
            services.AddSingleton<INavigationService, NavigationService>();
            return services;
        }
    }
}