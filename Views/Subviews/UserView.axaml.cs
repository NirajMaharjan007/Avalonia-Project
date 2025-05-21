using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace MyApp.Views.Subviews
{
    public partial class UserView : UserControl
    {
        public UserView()
        {
            AvaloniaXamlLoader.Load(this);
            InitializeComponent();

            AttachedToVisualTree += (_, __) =>
            {
                if (this.GetVisualRoot() is Window window)
                {
                    void updateMinWidth(object? sender, AvaloniaPropertyChangedEventArgs e)
                    {
                        if (e.Property == Window.WidthProperty)
                        {
                            CreateUserForm.MinWidth = window.Width - 360;
                        }
                    }

                    // Initial set
                    CreateUserForm.MinWidth = window.Width - 360;

                    // Subscribe to changes
                    window.PropertyChanged += updateMinWidth;
                }
            };
        }
    }
}
