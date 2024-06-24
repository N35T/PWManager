using PWManager.Application.Context;
using PWManager.Application.Services.Interfaces;
using PWManager.Avalonia.Controls.BaseClass;
using PWManager.Avalonia.Models;
using PWManager.UI;
using PWManager.UI.Environment.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PWManager.Avalonia.Controls;

public partial class SideNav : CustomControl {

    private readonly IStatusEnvironment _env;
    private readonly IUserEnvironment _userEnv;
    private readonly IGroupService _groupService;
    public ObservableCollection<GroupDisplayModel> AvailableGroups { get; set; } 

    public SideNav() {
        _env = IoC.Resolve<IStatusEnvironment>();
        _userEnv = IoC.Resolve<IUserEnvironment>();
        _groupService = IoC.Resolve<IGroupService>();
        _env.CurrentGroupUpdated += OnCurrentGroupUpdate;

        InitializeComponent();
        DataContext = this;
        InitializeGroupDisplay();
    }

    private void OnCurrentGroupUpdate() {
        var currentGroup = _env.CurrentGroup?.Identifier!;

        foreach (var group in AvailableGroups)
        {
            if (group.Identifier.Equals(currentGroup)) {
                group.IsActiveGroup = true;
            } else {
                group.IsActiveGroup = false;
            }
        }
        OnPropertyChanged(nameof(AvailableGroups));
    }

    private void InitializeGroupDisplay() {

        var groups = _groupService.GetAllGroupNames();

        AvailableGroups = new ObservableCollection<GroupDisplayModel>(
            groups
                .Select(e => new GroupDisplayModel { Identifier = e })
                .OrderByDescending(e => e.IsMainGroup)
                .ThenBy(e => e.Identifier)
                .ToList()
        );

        OnCurrentGroupUpdate();
        OnPropertyChanged(nameof(AvailableGroups));
    }

    public void OnAdd() {
        var test = Guid.NewGuid().ToString();
        _groupService.AddGroup(_userEnv.CurrentUser?.Id!, test);
        AvailableGroups.Add(new GroupDisplayModel { Identifier = test });
    }
}