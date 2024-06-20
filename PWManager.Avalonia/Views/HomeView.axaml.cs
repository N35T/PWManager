using Avalonia.Controls;
using Avalonia.Interactivity;
using PWManager.UI.ViewModels;
using System;

namespace PWManager.Avalonia.Views;

public partial class HomeView : UserControl {

    private const int CollapseSideNavBreakpoint = 500;
    
    public HomeView() {
        InitializeComponent();

        this.SizeChanged += QuerySize;
        container.IsPaneOpen = this.Width > CollapseSideNavBreakpoint;

        topnav.ToggleNavAction += ToggleSideNav;
    }

    private void ToggleSideNav() {
        container.IsPaneOpen = !container.IsPaneOpen;
    }

    private void QuerySize(object? sender, SizeChangedEventArgs e) {
        var size = e.NewSize;

        if (size.Width > CollapseSideNavBreakpoint) {
            this.container.Classes.Remove("collapse-side-nav");
            container.IsPaneOpen = true;
        } else {
            this.container.Classes.Add("collapse-side-nav");
            container.IsPaneOpen = false;
        }
    }

    public void AddAccountAction() {
        (this.DataContext as HomeViewModel)?.AddAccountAction();
    }
    public void SettingsAction() {
        (this.DataContext as HomeViewModel)?.SettingsAction();
    }
}