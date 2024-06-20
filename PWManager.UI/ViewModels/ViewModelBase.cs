using System.ComponentModel;

namespace PWManager.UI.ViewModels {
    public class ViewModelBase : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaiseAndSetIfChanged<T>(ref T field, T value, string fieldName) {
            if (field is not null && field.Equals(value)) {
                return;
            }

            field = value;
            OnPropertyChanged(fieldName);
        }
    }
}
