using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyApp.Views.Subviews
{
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
