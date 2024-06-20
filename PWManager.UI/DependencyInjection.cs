
using Microsoft.Extensions.DependencyInjection;
using PWManager.UI.Interfaces;
using PWManager.UI.ViewModels;

namespace PWManager.UI {
    public static class DependencyInjection {

        public static IServiceCollection AddUIServices(this IServiceCollection services) {

            var mainViewModel = new MainWindowViewModel();

            services.AddSingleton<IViewNavigator>(mainViewModel);

            // ViewModels
            services.AddSingleton<MainWindowViewModel>(mainViewModel);
            services.AddTransient<HomeViewModel>();
            services.AddTransient<LoginViewModel>();

            return services;
        }

        public static void RegisterDependenciesInIoC(this IServiceCollection services) {
            IoC.RegisterDependencies(services);
        }
    }
}
