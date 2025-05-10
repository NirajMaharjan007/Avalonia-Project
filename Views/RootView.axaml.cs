using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MyApp.ViewModels;

namespace MyApp.Views
{
    public partial class RootView : UserControl
    {
        public RootView()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new MainViewModel();
        }
    }
}
