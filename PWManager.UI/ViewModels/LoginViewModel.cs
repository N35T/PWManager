using PWManager.Application.Exceptions;
using PWManager.Application.Services.Interfaces;
using PWManager.Data.Abstraction;
using PWManager.UI.Interfaces;

namespace PWManager.UI.ViewModels {
    public class LoginViewModel : ViewModelBase {

        private readonly IViewNavigator _viewNavigator;
        private readonly ILoginService _loginService;
        private readonly IChooseFile _fileChooserService;

        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public LoginViewModel(IViewNavigator viewNavigator) {
            _loginService = IoC.Resolve<ILoginService>();
            _fileChooserService = IoC.Resolve<IChooseFile>();
            _viewNavigator = viewNavigator;

            if (ConfigFileHandler.DefaultFileExists()) {
                var lastUser = ConfigFileHandler.ReadDefaultFile().Split('\n');

                if (lastUser.Length == 2) {
                    UserName = lastUser[0];
                }
            }
        }

        public async void Login() {
            var path = "";

            if (ConfigFileHandler.DefaultFileExists()) {
                var lastUser = ConfigFileHandler.ReadDefaultFile().Split('\n');

                if (lastUser.Length == 2) {
                    path = lastUser[1];
                }
            }

            if (String.IsNullOrWhiteSpace(UserName)) {
                return;
            }
            if (String.IsNullOrWhiteSpace(Password)) {
                return;
            }
            if (String.IsNullOrWhiteSpace(path)) {
                path = await _fileChooserService.OpenFileChooser();
            }

            try {
                var succ = _loginService.Login(UserName, Password, path); 
                if (!succ) {
                    // show error message -> Username or password incorrect
                    return;
                }
                ConfigFileHandler.WriteDefaultFile(UserName, path);
                _viewNavigator.GoToPage<HomeViewModel>();
            }
            catch (UserFeedbackException ex) {
                // catch user feedback exception and show message
            }

        }
    }
}
