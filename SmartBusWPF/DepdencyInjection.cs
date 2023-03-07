using Mapster;
using MapsterMapper;
using System.Reflection;
using SmartBusWPF.Services;
using SmartBusWPF.ViewModels;
using SmartBusWPF.Services.API;
using SmartBusWPF.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using SmartBusWPF.Common.Interfaces.Services.API;

namespace SmartBusWPF
{
    public static class DepdencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }

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
            //API Services
            services.AddSingleton<IHttpClientService, HttpClientService>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<ITripService, TripService>();

            //App Services
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<IProcessMonitorService, ProcessMonitorService>();
            services.AddSingleton<ICryptographyService, CryptographyService>();
            services.AddSingleton<IServerSocketService, ServerSocketService>();
            services.AddSingleton<IClientSocketService, ClientSocketService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IHuskyLensService, HuskyLensService>();
            services.AddSingleton<IGeoLocationService, GeoLocationService>();
            
            return services;
        }
    }
}