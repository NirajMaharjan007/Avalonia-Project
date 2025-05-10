using System;
using Avalonia.Controls;
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

        Width = 512;
        Height = 550;

        Console.WriteLine(MinWidth + " " + MinHeight);
        // Listen to login success event
        if (_loginView.DataContext is LoginViewModel loginVm)
            loginVm.LoginSucceeded += OnLoginSucceeded;

        CanResize = false;
        // Show login view initially
        Content = _loginView;
    }

    private void OnLoginSucceeded(object? sender, EventArgs e)
    {
        Content = _rootView;
        Width = 800;
        Height = 600;
        CanResize = true;
    }
}
