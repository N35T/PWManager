using PWManager.Application.Exceptions;
using PWManager.Application.Services.Interfaces;
using PWManager.Data.Abstraction;
using PWManager.UI.Interfaces;

namespace PWManager.UI.ViewModels {
    public class InitViewModel : ViewModelBase {

        private readonly IViewNavigator _viewNavigator;
        private readonly ILoginService _loginService;
        private readonly IChooseFile _fileChooserService;
        private readonly IDatabaseInitializerService _dbInit;

        public string DBPath { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string PasswordRepeat { get; set; } = "";

        public InitViewModel(IViewNavigator viewNavigator) {
            _loginService = IoC.Resolve<ILoginService>();
            _fileChooserService = IoC.Resolve<IChooseFile>();
            _dbInit = IoC.Resolve<IDatabaseInitializerService>();
            _viewNavigator = viewNavigator;
        }

        public async void GetPath() {
            var path = await _fileChooserService.SaveFileChooser();
            if (!Path.Exists(path)) {
                return;
            }
            DBPath = Path.GetFullPath(path);
        }

        public void Init() {
            var path = DBPath;
            if (!Path.Exists(path)) {
                return;
            }

            try {
                _dbInit.CheckIfDataBaseExistsOnPath(path);
            } catch (UserFeedbackException ex) {
                return;
            }

            if (!Password.Equals(PasswordRepeat)) {
                return;
            }

            try {
                _dbInit.InitDatabase(path, UserName!, Password!);
            } catch (UserFeedbackException ex) {
                return;
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