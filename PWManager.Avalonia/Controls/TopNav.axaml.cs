using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using PWManager.Avalonia.Controls.BaseClass;
using PWManager.UI;
using PWManager.UI.Environment.Interfaces;
using PWManager.UI.Extensions;
using System;

namespace PWManager.Avalonia.Controls;

public partial class TopNav : CustomControl {

    public event Action? ToggleNavAction;
    public event Action? AddAccountAction;
    public event Action? SettingsAction;

    private IStatusEnvironment _statusEnv;

    public TopNav() {
        InitializeComponent();

        _statusEnv = IoC.Resolve<IStatusEnvironment>();

        _statusEnv.CurrentGroupUpdated += ClearFilter;

        accountSearch.KeyUp += UpdateFilter;

        sidenavToggleButton.Click += (_,_) => ToggleNavAction?.SafeTrigger();
        addButton.Click += (_,_) => AddAccountAction?.SafeTrigger();
        settingsButton.Click += (_, _) => SettingsAction?.SafeTrigger();
    }

    private void UpdateFilter(object? sender, KeyEventArgs e) {
        _statusEnv.OnAccountFilterUpdated(accountSearch.Text ?? string.Empty);
    }

    private void ClearFilter() {
        accountSearch.Clear();
    }
}