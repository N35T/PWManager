using Avalonia.Interactivity;
using PWManager.Avalonia.Controls.BaseClass;
using PWManager.UI.Extensions;
using System;

namespace PWManager.Avalonia.Controls;

public partial class TopNav : CustomControl {

    public event Action? ToggleNavAction;
    public event Action? AddAccountAction;
    public event Action? SettingsAction;

    public TopNav() {
        InitializeComponent();

        sidenavToggleButton.Click += (_,_) => ToggleNavAction?.SafeTrigger();
        addButton.Click += (_,_) => AddAccountAction?.SafeTrigger();
        settingsButton.Click += (_, _) => SettingsAction?.SafeTrigger();
    }    
}