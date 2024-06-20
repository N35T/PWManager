
using PWManager.UI.Interfaces;

namespace PWManager.UI.ViewModels {
    public class LoginViewModel : ViewModelBase {

        private readonly IViewNavigator _viewNavigator;

        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public LoginViewModel(IViewNavigator viewNavigator) {
            _viewNavigator = viewNavigator;
        }

        public void Login() {
            _viewNavigator.GoToPage<HomeViewModel>();
        }
    }
}
