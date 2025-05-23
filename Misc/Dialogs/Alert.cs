using Material.Dialog;
using Material.Dialog.Views;
using static System.Console;

namespace MyApp.Misc.Dialogs;

public class Alert : AlertDialogBuilderParams
{
    private readonly AlertDialog _dialog = new();

    public Alert()
    {
        _dialog.InitializeComponent(true);
        _dialog.Activate();
        _dialog.Content = this;
        _dialog.IsVisible = true;
        _dialog.WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner;
    }

    public void SetPostion(int x, int y)
    {
        WriteLine($"Position Set to {x}, {y}");
        _dialog.Position = new Avalonia.PixelPoint(x, y);
    }

    public bool Visible => _dialog.IsVisible;
}
