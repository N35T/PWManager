using PWManager.Application.Exceptions;
using PWManager.Application.Services.Interfaces;
using PWManager.Data.Abstraction;
using PWManager.UI.Interfaces;

namespace PWManager.UI.ViewModels {
    public class WelcomeViewModel : ViewModelBase {

        private readonly IViewNavigator _viewNavigator;

        public WelcomeViewModel(IViewNavigator viewNavigator) {
            _viewNavigator = viewNavigator;
        }

        public void Login() {
            _viewNavigator.GoToPage<LoginViewModel>();
        }

        public void Init() {
            _viewNavigator.GoToPage<InitViewModel>();
        }
    }
}