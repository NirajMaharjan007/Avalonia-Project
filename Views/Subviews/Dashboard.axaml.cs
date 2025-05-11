using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MyApp.ViewModels;

namespace MyApp.Views.Subviews
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new MainViewModel();
        }
    }
}
