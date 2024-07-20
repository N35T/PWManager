using PWManager.Application.Exceptions;
using PWManager.Application.Services.Interfaces;
using PWManager.Data.Abstraction;
using PWManager.UI.Interfaces;
using System.IO;

namespace PWManager.UI.ViewModels {
    public class LoginViewModel : ViewModelBase {

        private readonly IViewNavigator _viewNavigator;
        private readonly ILoginService _loginService;
        private readonly IChooseFile _fileChooserService;
        private Task<string> _pathTask;
        private string _path;

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
                    _path = lastUser[1];
                }
            }

            if (String.IsNullOrWhiteSpace(_path)) {
                _pathTask = _fileChooserService.OpenFileChooser();
            }
        }

        public void Login() {

            if (String.IsNullOrWhiteSpace(UserName)) {
                return;
            }
            if (String.IsNullOrWhiteSpace(Password)) {
                return;
            }
            if (String.IsNullOrWhiteSpace(_path)) {
                _path = _pathTask.Result;
            }
            if (String.IsNullOrWhiteSpace(_path)) {
                return;
            }

            try {
                var succ = _loginService.Login(UserName, Password, _path); 
                if (!succ) {
                    // show error message -> Username or password incorrect
                    return;
                }
                ConfigFileHandler.WriteDefaultFile(UserName, _path);
                _viewNavigator.GoToPage<HomeViewModel>();
            }
            catch (UserFeedbackException ex) {
                // catch user feedback exception and show message
            }

        }
    }
}
