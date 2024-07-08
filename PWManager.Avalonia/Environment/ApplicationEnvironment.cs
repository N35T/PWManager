
using Avalonia.Controls;
using PWManager.Application.Context;
using PWManager.Avalonia.Environment.Interfaces;
using PWManager.Domain.Entities;
using PWManager.UI.Environment.Interfaces;
using System;

namespace PWManager.Avalonia.Environment {
    internal class ApplicationEnvironment : IStatusEnvironment, IUserEnvironment, ICryptEnvironment, ICliEnvironment, IVisualEnvironment {

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

        private Group? _currentGroup;
        public Group? CurrentGroup {
            get => _currentGroup;
            set {
                if (_currentGroup == value) {
                    return;
                }
                _currentGroup = value;
                if (StatusEnvironmentUpdated is not null) {
                    StatusEnvironmentUpdated(nameof(CurrentGroup));
                }
                if (CurrentGroupUpdated is not null) {
                    CurrentGroupUpdated();
                }
            }
        }

        public User? CurrentUser { get; set ; }
        public Settings? UserSettings { get; set; }
        public string? EncryptionKey { get; set; }
        public bool RunningSession { get; set; }
        public TopLevel? CurrentTopLevel { get; set; }

        public event Action<string> StatusEnvironmentUpdated;
        public event Action<string> UserEnvironmentUpdated;
        public event Action<string> CryptEnvironmentUpdated;
        public event Action CurrentGroupUpdated;

        public void WritePrompt() {
            throw new NotImplementedException();
        }
    }
}
