using Avalonia.Controls;
using PWManager.Avalonia.Environment.Interfaces;
using PWManager.UI;
using System;

namespace PWManager.Avalonia.Views {
    public partial class MainWindow : Window {
        private readonly IVisualEnvironment _visEnv;
        public MainWindow() {
            InitializeComponent();
            _visEnv = IoC.Resolve<IVisualEnvironment>();
            _visEnv.CurrentTopLevel = GetTopLevel(this) ?? throw new ApplicationException("Window not found");
        }
    }
}