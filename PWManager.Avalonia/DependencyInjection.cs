using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using PWManager.Application.Context;
using PWManager.Avalonia.Environment;
using PWManager.Avalonia.Environment.Interfaces;
using PWManager.Avalonia.Services;
using PWManager.UI.Environment.Interfaces;
using PWManager.UI.Interfaces;

namespace PWManager.Avalonia {
    internal static class DependencyInjection {

        public static IServiceCollection AddEnvironment(this IServiceCollection services) {

            var env = new ApplicationEnvironment();

            services.AddSingleton<IStatusEnvironment>(env);
            services.AddSingleton<IUserEnvironment>(env);
            services.AddSingleton<ICryptEnvironment>(env);
            services.AddSingleton<ICliEnvironment>(env);
            services.AddSingleton<IVisualEnvironment>(env);

            services.AddTransient<IChooseFile, FileChooserService>();

            return services;
        }
    }
}
