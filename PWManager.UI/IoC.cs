
using Microsoft.Extensions.DependencyInjection;

namespace PWManager.UI {
    public static class IoC {

        private static IServiceProvider _services = null!;

        public static T Resolve<T>() where T : notnull {
            if(_services is null) {
                throw new ApplicationException("No services registered with the IoC!");
            }

            return _services.GetRequiredService<T>();
        }

        public static void RegisterDependencies(IServiceCollection collection) {
            _services = collection.BuildServiceProvider();
        }

    }
}
