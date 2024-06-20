using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PWManager.UI.ViewModels;
using PWManager.Avalonia.Views;
using PWManager.UI;
using Microsoft.Extensions.DependencyInjection;

namespace PWManager.Avalonia {
    public partial class App : global::Avalonia.Application {
        public override void Initialize() {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted() {

            // Initialize dependencies
            var services = new ServiceCollection();

            services
                .AddUIServices()
                .AddEnvironment()
                .RegisterDependenciesInIoC();
            


            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                desktop.MainWindow = new MainWindow {
                    DataContext = IoC.Resolve<MainWindowViewModel>(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}