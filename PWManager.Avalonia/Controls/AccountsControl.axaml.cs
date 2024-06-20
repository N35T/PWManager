using Avalonia.Controls;
using PWManager.Avalonia.Controls.BaseClass;
using System;

namespace PWManager.Avalonia.Controls;

public partial class AccountsControl : CustomControl {

    public AccountsControl() {
        InitializeComponent();
        DataContext = this;

        this.SizeChanged += UpdateSize;
    }


    private void UpdateSize(object? sender, SizeChangedEventArgs e) {
        // splitPane.OpenPaneLength = Max
    }
}