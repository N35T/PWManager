using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace PWManager.Avalonia.Views;

public partial class WelcomeView : UserControl {

    public WelcomeView() {
        InitializeComponent();
        this.SizeChanged += QuerySize;
    }

    private void QuerySize(object? sender, SizeChangedEventArgs e) {
        var size = e.NewSize;

        if (size.Width > 500) {
            container.Classes.Remove("small-layout");
        } else {
            container.Classes.Add("small-layout");
        }
    }
}