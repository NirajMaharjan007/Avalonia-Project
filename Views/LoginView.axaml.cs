using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MyApp.ViewModels;

namespace MyApp.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new LoginViewModel();
        }
    }
}
