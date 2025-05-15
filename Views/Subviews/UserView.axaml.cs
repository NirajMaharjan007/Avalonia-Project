using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyApp.Views.Subviews
{
    public partial class UserView : UserControl
    {
        public UserView()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
