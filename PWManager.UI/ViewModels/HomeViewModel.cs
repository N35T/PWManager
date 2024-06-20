
using PWManager.UI.Environment.Interfaces;
using PWManager.UI.Interfaces;

namespace PWManager.UI.ViewModels {
    public class HomeViewModel : ViewModelBase {

        private readonly IViewNavigator _viewNavigator;
        private readonly IStatusEnvironment _statusEnv;

        public bool Synchronizing { get => _statusEnv.Synchronizing; set => _statusEnv.Synchronizing = value; }

        public HomeViewModel(IViewNavigator viewNavigator, IStatusEnvironment statusEnv) {
            _viewNavigator = viewNavigator;
            _statusEnv = statusEnv;

            _statusEnv.StatusEnvironmentUpdated += OnPropertyChanged;
        }

        public void TestNavigation() {
            //(_viewNavigator.GoToPage<LoginViewModel>();
            Synchronizing = !Synchronizing;
        }

        public void AddAccountAction() {
        }

        public void SettingsAction() {
        }
    }
}
