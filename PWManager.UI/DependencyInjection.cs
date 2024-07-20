
using Microsoft.Extensions.DependencyInjection;
using PWManager.Application;
using PWManager.Data;
using PWManager.UI.Interfaces;
using PWManager.UI.ViewModels;

namespace PWManager.UI {
    public static class DependencyInjection {

        public static IServiceCollection AddUIServices(this IServiceCollection services) {

            var mainViewModel = new MainWindowViewModel();

            services.AddSingleton<IViewNavigator>(mainViewModel);

            // ViewModels
            services.AddTransient<HomeViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<InitViewModel>();
            services.AddTransient<WelcomeViewModel>();
            services.AddApplicationServices();
            services.AddDataServices();
            services.AddSingleton(mainViewModel);

            return services;
        }

        public static void RegisterDependenciesInIoC(this IServiceCollection services) {
            IoC.RegisterDependencies(services);
        }
    }
}
