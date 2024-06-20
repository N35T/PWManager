using PWManager.UI.Interfaces;

namespace PWManager.UI.ViewModels {
    public class MainWindowViewModel : ViewModelBase, IViewNavigator {

        private ViewModelBase _viewModelContent = null!;
        
        public ViewModelBase ViewModelContent { 
            get => _viewModelContent ?? IoC.Resolve<LoginViewModel>(); 
            private set => this.RaiseAndSetIfChanged(ref _viewModelContent, value, nameof(ViewModelContent)); 
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
