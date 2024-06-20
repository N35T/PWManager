using PWManager.Application.Services.Interfaces;
using PWManager.UI;
using System;
using System.ComponentModel;

namespace PWManager.Avalonia.Models {
    public class GroupDisplayModel : INotifyPropertyChanged {

        public string Id { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public bool IsMainGroup { get; set; }
        private bool _isActiveGroup;
        public bool IsActiveGroup { get => _isActiveGroup; set { 
                if (value == _isActiveGroup) { 
                    return; 
                } 
                _isActiveGroup = value;
                OnPropertyChanged(nameof(IsActiveGroup));
            } 
        }
        public bool IsRemoteGroup { get; set; }

        public bool IsNormalGroup { get => !IsMainGroup && !IsRemoteGroup; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnClick() {
            if (!IsActiveGroup) {
                IoC.Resolve<IGroupService>().SwitchGroup(Identifier); // zu Event tauschen
            }
        }

        public void OnDelete() {
            // Event Triggern -> Name mit geben
            // User PW abfrage
            // ist das Main group? -> Ja -> neue Main group festlegen/erstellen
                               //  -> Nein -> weiter
            // zur main group wechseln
            // Eintrag aus SideNav löschen
            // Gruppe in DB löschen
        }
    }
}
