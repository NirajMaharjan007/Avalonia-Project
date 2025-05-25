using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Material.Dialog;
using Material.Dialog.Views;
using MyApp.Misc.Dialogs.Views;
using static System.Console;

namespace MyApp.Misc.Dialogs;

public class Alert : AlertDialogBuilderParams
{
    public enum Type
    {
        Success,
        Warning,
        Error,
        Info,

        Confirmation,
    }

    private readonly AlertDialog _dialog = new();

    public Alert(Type type, string message)
    {
        _dialog.InitializeComponent(true);
        _dialog.Activate();
        switch (type)
        {
            case Type.Success:
                Title = "Success";
                Success success = new();
                success.ContentText.Text = message;
                success.OkayButton.Click += (sender, e) => _dialog.Close();

                _dialog.Title = "Successful Operation";
                _dialog.Content = success;

                break;

            case Type.Error:
                Title = "Failed";
                Failed fail = new();
                fail.ContentText.Text = message;
                fail.OkayButton.Click += (sender, e) => _dialog.Close();

                _dialog.Title = "Failed Operation";
                _dialog.Content = fail;

                break;
        }
        _dialog.IsVisible = true;
        _dialog.CanResize = false;

        Center();
    }

    public void SetPostion(int x, int y)
    {
        WriteLine($"Position Set to {x}, {y}");
        _dialog.Position = new PixelPoint(x, y);
    }

    private void Center()
    {
        _dialog.WindowStartupLocation = WindowStartupLocation.Manual;
        if (
            Application.Current?.ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktopLifetime
        )
        {
            var window = desktopLifetime.MainWindow;

            if (window is not null)
            {
                int x = (int)window.Width / 2 + 200,
                    y = (int)window.Height / 2 - 100;
                SetPostion(x, y);
            }
        }
    }

    public bool Visible => _dialog.IsVisible;

    public Window Dialog => _dialog;

    public bool CanResize => _dialog.CanResize;
}
