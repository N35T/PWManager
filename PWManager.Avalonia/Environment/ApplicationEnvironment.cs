
using PWManager.Application.Context;
using PWManager.Domain.Entities;
using PWManager.UI.Environment.Interfaces;
using System;

namespace PWManager.Avalonia.Environment {
    internal class ApplicationEnvironment : IStatusEnvironment, IUserEnvironment, ICryptEnvironment {

        private bool _isConnected = false;
        public bool Connected {
            get => _isConnected;
            set {
                if (_isConnected == value) {
                    return;
                }
                _isConnected = value;
                StatusEnvironmentUpdated(nameof(Connected));
            }
        }

        private bool _isSynchronizing = false;
        public bool Synchronizing {
            get => _isSynchronizing;
            set {
                if(_isSynchronizing == value) {
                    return;
                }
                _isSynchronizing = value;
                StatusEnvironmentUpdated(nameof(Synchronizing));
            }
        }

        private string _currentGroup = "main";
        public string CurrentGroup {
            get => _currentGroup;
            set {
                if (_currentGroup == value) {
                    return;
                }
                _currentGroup = value;
                StatusEnvironmentUpdated(nameof(CurrentGroup));
            }
        }

        public User? CurrentUser { get; set ; }
        Group? IUserEnvironment.CurrentGroup { get; set ; }
        public Settings? UserSettings { get; set; }
        public string? EncryptionKey { get; set; }

        public event Action<string> StatusEnvironmentUpdated;
    }
}
