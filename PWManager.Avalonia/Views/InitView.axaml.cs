using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace PWManager.Avalonia.Views;

public partial class InitView : UserControl {

    public InitView() {
        InitializeComponent();
        this.SizeChanged += QuerySize;
        loginBtn.Click += Clicked;
    }

    private void HighlightIfEmpty(object? sender, TextChangedEventArgs e) {
        HighlightIfEmpty();
    }

    private void Clicked(object? sender, RoutedEventArgs e) {
        HighlightIfEmpty();

        nameBox.TextChanged += HighlightIfEmpty;
        pwBox.TextChanged += HighlightIfEmpty;
        pathBox.TextChanged += HighlightIfEmpty;
        pwBoxRepeat.TextChanged += HighlightIfEmpty;
    }

    private void HighlightIfEmpty() {
        if (String.IsNullOrWhiteSpace(pathBox.Text)) {
            pathBox.Classes.Add("empty");
        } else {
            pathBox.Classes.Remove("empty");
        }
        if (String.IsNullOrWhiteSpace(nameBox.Text)) {
            nameBox.Classes.Add("empty");
        } else {
            nameBox.Classes.Remove("empty");
        }
        if (String.IsNullOrWhiteSpace(pwBox.Text)) {
            pwBox.Classes.Add("empty");
        } else {
            pwBox.Classes.Remove("empty");
        }
        if (String.IsNullOrWhiteSpace(pwBoxRepeat.Text)) {
            pwBoxRepeat.Classes.Add("empty");
        } else {
            pwBoxRepeat.Classes.Remove("empty");
        }
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