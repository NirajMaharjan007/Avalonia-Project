using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyApp.Views
{
    public partial class RootView : UserControl
    {
        public RootView()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
