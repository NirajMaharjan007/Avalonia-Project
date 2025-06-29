using System;
using Avalonia.Controls;

namespace MyApp.Misc.Dialogs.Views;

public partial class Warning : UserControl
{
    public Warning()
    {
        InitializeComponent();
        Focus();

        Width = 280;
        Height = 250;

        ConfirmPanel.IsVisible = false;
    }

    public Warning(bool confirm)
        : this()
    {
        if (confirm)
        {
            ConfirmPanel.IsVisible = true;
            OkayButton.IsVisible = false;
        }
        else
        {
            ConfirmPanel.IsVisible = false;
            OkayButton.IsVisible = true;
        }
    }
}
