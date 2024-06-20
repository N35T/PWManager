using PWManager.Avalonia.Controls.BaseClass;
using PWManager.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PWManager.Avalonia.Controls;

public partial class SideNav : CustomControl {
    
    public ObservableCollection<GroupDisplayModel> AvailableGroups { get; set; }

    public SideNav() {
        InitializeComponent();
        DataContext = this;
        InitializeDebugGroupDisplay();
    }

    private void InitializeDebugGroupDisplay() {
        var groups = new GroupDisplayModel[] {
            new() { Id = Guid.NewGuid().ToString(), Identifier = "main", IsMainGroup = true },
            new() { Id = Guid.NewGuid().ToString(), Identifier = "finanzen", IsRemoteGroup = true },
            new() { Id = Guid.NewGuid().ToString(), Identifier = "gamesaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" },
            new() { Id = Guid.NewGuid().ToString(), Identifier = "ssh", IsRemoteGroup = true },
            new() { Id = Guid.NewGuid().ToString(), Identifier = "uni", IsActiveGroup = true },
            new() { Id = Guid.NewGuid().ToString(), Identifier = "website" }
        };
        AvailableGroups = new ObservableCollection<GroupDisplayModel>(
            groups.OrderByDescending(e => e.IsMainGroup)
                .ThenBy(e => e.Identifier)
                .ToList()
        );

        OnPropertyChanged(nameof(AvailableGroups));
    }
}