using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MyApp.ViewModels;

namespace MyApp.Views
{
    public partial class RootView : UserControl
    {
        private readonly RootViewModel _root = new();

        public RootView()
        {
            AvaloniaXamlLoader.Load(this);

            DataContext = _root;
        }
    }
}
