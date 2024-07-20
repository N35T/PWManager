using PWManager.Data.Abstraction;
using PWManager.UI.Interfaces;
using System.IO;

namespace PWManager.UI.ViewModels {
    public class MainWindowViewModel : ViewModelBase, IViewNavigator {

        private ViewModelBase _viewModelContent = null!;
        private string _path = "";

        public MainWindowViewModel() {
            if (ConfigFileHandler.DefaultFileExists()) {
                var lastUser = ConfigFileHandler.ReadDefaultFile().Split('\n');

                if (lastUser.Length == 2) {
                    _path = lastUser[1];
                }
            }
        }

        public ViewModelBase ViewModelContent {
            get => GetViewModel();
            private set => this.RaiseAndSetIfChanged(ref _viewModelContent, value, nameof(ViewModelContent)); 
        }

        private ViewModelBase GetViewModel() {
            if (!String.IsNullOrEmpty(_path)) {
                return IoC.Resolve<LoginViewModel>();
            } else {
                return _viewModelContent ?? IoC.Resolve<WelcomeViewModel>();
            }
        }

        public void GoToPage<T>() where T : ViewModelBase {
            var viewModel = (ViewModelBase)IoC.Resolve<T>();
            if (viewModel is null) {
                throw new ApplicationException("View model type not found in the DI!");
            }
            ViewModelContent = viewModel;
        }
    }
}
