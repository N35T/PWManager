using PWManager.UI.ViewModels;

namespace PWManager.UI.Interfaces {
    public interface IViewNavigator {

        void GoToPage<T>() where T : ViewModelBase;
    }
}
