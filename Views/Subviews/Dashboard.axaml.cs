using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using MyApp.ViewModels;

namespace MyApp.Views.Subviews
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            AttachedToVisualTree += (_, __) =>
            {
                if (this.GetVisualRoot() is Window window)
                {
                    void updateMinWidth(object? sender, AvaloniaPropertyChangedEventArgs e)
                    {
                        if (e.Property == Window.WidthProperty)
                        {
                            MainLayout.MinWidth = window.Width - 360;
                        }
                    }

                    // Initial set
                    MainLayout.MinWidth = window.Width - 360;

                    // Subscribe to changes
                    window.PropertyChanged += updateMinWidth;
                }
            };
        }
    }
}
