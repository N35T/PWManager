using Avalonia.Controls;
using PWManager.Application.Context;
using PWManager.Avalonia.Controls.BaseClass;
using PWManager.Avalonia.Models;
using PWManager.UI;
using PWManager.UI.Environment.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PWManager.Avalonia.Controls;

public partial class AccountsControl : CustomControl {

    public ObservableCollection<AccountDisplayModel> Accounts {get;set;}

    public AccountDisplayModel? CurrentlyOpenedAccount {get;set;}

    private TopLevel _window = null!;
    private IStatusEnvironment _statusEnv;

    public AccountsControl() {
        InitializeComponent();
        this.DataContext = this;

        this.SizeChanged += UpdateSize;

        _statusEnv = IoC.Resolve<IStatusEnvironment>();

        _statusEnv.CurrentGroupUpdated += UpdateAccounts;
        UpdateAccounts();

        this.Initialized += OnInit;
    }

    private void UpdateAccounts() {
        if(_statusEnv.CurrentGroup is null) {
            return;
        }
        this.Accounts = new ObservableCollection<AccountDisplayModel>(_statusEnv.CurrentGroup.Accounts.Select(e => new AccountDisplayModel {AccountName = e.Identifier, Password = e.Password, LoginName = e.LoginName}));
        OnPropertyChanged(nameof(Accounts));
    }

    private void OnInit(object? sender, EventArgs e) {
        _window = TopLevel.GetTopLevel(this) ?? throw new ApplicationException("Window not found");
    }

    private void UpdateSize(object? sender, SizeChangedEventArgs e) {
        // splitPane.OpenPaneLength = Max
    }

    public void OnAccountClick(AccountDisplayModel account) {
        splitPane.IsPaneOpen = true;
        CurrentlyOpenedAccount = account;

        OnPropertyChanged(nameof(CurrentlyOpenedAccount));
    }

    public void CloseSidePanel() {
        splitPane.IsPaneOpen = false;
        CurrentlyOpenedAccount = null;

        OnPropertyChanged(nameof(CurrentlyOpenedAccount));
    }

    public async Task CopyActiveLoginName() {
        if(_window.Clipboard is null || CurrentlyOpenedAccount is null) {
            return;
        }
        await _window.Clipboard.SetTextAsync(CurrentlyOpenedAccount.LoginName);
    }

    public async Task CopyActivePassword() {
        if(_window.Clipboard is null || CurrentlyOpenedAccount is null) {
            return;
        }
        await _window.Clipboard.SetTextAsync(CurrentlyOpenedAccount.Password);
    }
}