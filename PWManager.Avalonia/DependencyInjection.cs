using Microsoft.Extensions.DependencyInjection;
using PWManager.Application.Context;
using PWManager.Avalonia.Environment;
using PWManager.UI.Environment.Interfaces;

namespace PWManager.Avalonia {
    internal static class DependencyInjection {

        public static IServiceCollection AddEnvironment(this IServiceCollection services) {

            var env = new ApplicationEnvironment();

            services.AddSingleton<IStatusEnvironment>(env);
            services.AddSingleton<IUserEnvironment>(env);
            services.AddSingleton<ICryptEnvironment>(env);
            services.AddSingleton<ICliEnvironment>(env);

            return services;
        }
    }
}
