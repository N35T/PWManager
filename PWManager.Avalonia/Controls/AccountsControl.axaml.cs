using Avalonia.Controls;
using PWManager.Avalonia.Controls.BaseClass;
using PWManager.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PWManager.Avalonia.Controls;

public partial class AccountsControl : CustomControl {

    public ObservableCollection<AccountDisplayModel> Accounts {get;set;}

    public AccountDisplayModel? CurrentlyOpenedAccount {get;set;}

    private TopLevel _window = null!;

    public AccountsControl() {
        InitializeComponent();
        this.DataContext = this;

        this.SizeChanged += UpdateSize;

        Accounts = new ObservableCollection<AccountDisplayModel>(new List<AccountDisplayModel> {
            new AccountDisplayModel {AccountName = "TestAccount 1", LoginName = "account1", Password="pass1"},
            new AccountDisplayModel {AccountName = "TestAccount 2", LoginName = "account2", Password="pass2"},
            new AccountDisplayModel {AccountName = "TestAccount 3", LoginName = "account3", Password="pass3"},
        });

        OnPropertyChanged(nameof(Accounts));

        this.Initialized += OnInit;
    }

    private void OnInit(object? sender, EventArgs e) {
        Console.WriteLine("Here");
        _window = TopLevel.GetTopLevel(this) ?? throw new ApplicationException("Window not found");
        Console.WriteLine("Here!");    
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