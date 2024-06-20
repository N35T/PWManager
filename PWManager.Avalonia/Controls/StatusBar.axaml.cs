using PWManager.UI.Environment.Interfaces;
using PWManager.UI;
using PWManager.Avalonia.Controls.BaseClass;

namespace PWManager.Avalonia.Controls;

public partial class StatusBar : CustomControl {

    private readonly IStatusEnvironment _env;

    public bool Connected { get => _env.Connected; set => _env.Connected = value; }

    public bool Synchronizing { get => _env.Synchronizing; set => _env.Synchronizing = value; }

    public string CurrentGroup { get => _env.CurrentGroup; set => _env.CurrentGroup = value; }

    public StatusBar() {
        _env = IoC.Resolve<IStatusEnvironment>();
        _env.StatusEnvironmentUpdated += OnPropertyChanged;

        InitializeComponent();
        DataContext = this;
    }
}