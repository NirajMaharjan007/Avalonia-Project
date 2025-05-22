using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using static System.Console;

namespace MyApp.Misc.Dialogs;

public partial class Alert : Window
{
    public Alert()
    {
        AvaloniaXamlLoader.Load(this);
        Activate();
    }

    public void SetPostion(int x, int y)
    {
        WriteLine($"Position Set to {x}, {y}");
        Position = new Avalonia.PixelPoint(x, y);
    }
}
