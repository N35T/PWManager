
using PWManager.Application.Exceptions;
using PWManager.Application.Services.Interfaces;
using PWManager.Data.Services;
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
            IoC.Resolve<ILoginService>().Login(UserName, Password, "D:\\GitHub\\PWManager");
            _viewNavigator.GoToPage<HomeViewModel>();
            //try{
            //}
            //catch (UserFeedbackException ex) { 
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}
