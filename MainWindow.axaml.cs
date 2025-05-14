using System;
using Avalonia.Controls;
using MyApp.Services;
using MyApp.ViewModels;
using MyApp.Views;

namespace MyApp;

public partial class MainWindow : Window
{
    private readonly LoginView _loginView;
    private readonly RootView _rootView;

    public MainWindow()
    {
        InitializeComponent();

        _loginView = new LoginView();
        _rootView = new RootView();

        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        Width = 500;
        Height = 500;

        // Listen to login success event
        if (_loginView.DataContext is LoginViewModel loginVm)
            loginVm.LoginSucceeded += OnLoginSucceeded;

        CanResize = false;
        // Show login view initially
        Content = _loginView;

        Closing += (s, e) => Auth.Dispose();
    }

    private void OnLoginSucceeded(object? sender, EventArgs e)
    {
        Content = _rootView;
        Width = 1200;
        Height = 720;
        CanResize = true;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }
}
