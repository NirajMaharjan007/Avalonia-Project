using System;
using Avalonia.Controls;
using MyApp.ViewModels;
using MyApp.Views;

namespace MyApp;

public partial class MainWindow : Window
{
    private LoginView _loginView;
    private RootView _rootView;

    public MainWindow()
    {
        InitializeComponent();

        _loginView = new LoginView();
        _rootView = new RootView();

        var loginVm = _loginView.DataContext as LoginViewModel;

        // Listen to login success event
        if (loginVm != null)
            loginVm.LoginSucceeded += OnLoginSucceeded;

        // Show login view initially
        Content = _loginView;
    }

    private void OnLoginSucceeded(object? sender, EventArgs e)
    {
        Content = _rootView;
    }
}
