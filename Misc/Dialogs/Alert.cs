using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using Material.Dialog;
using Material.Dialog.Views;
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
    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public Alert()
    {
        Init();
    }

    public Alert(Type type)
    {
        Init();
        switch (type)
        {
            case Type.Success:
                Title = "Success";
                Message = "Operation completed successfully.";
                _dialog.Title = Title;

                break;
        }
    }

    private void Init()
    {
        _dialog.InitializeComponent(true);
        _dialog.Activate();
        _dialog.Content = this;
        _dialog.IsVisible = true;
        _dialog.CanResize = false;

        Center();
    }

    public void SetPostion(int x, int y)
    {
        WriteLine($"Position Set to {x}, {y}");
        _dialog.Position = new Avalonia.PixelPoint(x, y);
    }

    private void Center()
    {
        WriteLine($"Centering");

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

    public bool CanResize => _dialog.CanResize;
}
