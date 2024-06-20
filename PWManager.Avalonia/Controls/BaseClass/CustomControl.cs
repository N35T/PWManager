using Avalonia.Controls;
using System.ComponentModel;

namespace PWManager.Avalonia.Controls.BaseClass {
    public abstract class CustomControl : UserControl, INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
