using Avalonia.Controls;

namespace MyApp.Misc.Dialogs.Views;

public partial class Warning : UserControl
{
    public Warning()
    {
        InitializeComponent();
        Focus();

        Width = 250;
        Height = 180;
    }
}
