using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Material.Styles.Assists;
using MyApp.ViewModels;

namespace MyApp.Views.Subviews
{
    public partial class UserView : UserControl
    {
        private readonly UserViewModel _content = new();

        private void Verification()
        {
            EmailBox.PropertyChanged += (sender, args) =>
            {
                if (args.Property == TextBox.TextProperty)
                {
                    if (!_content.IsValidEmail)
                    {
                        EmailBox.SetValue(
                            TextFieldAssist.HintsProperty,
                            "Enter a valid email address"
                        );
                    }
                    else
                        EmailBox.ClearValue(TextFieldAssist.HintsProperty);
                }
            };

            ConfirmPassword.PropertyChanged += (sender, args) =>
            {
                if (args.Property == TextBox.TextProperty)
                {
                    if (!_content.IsSamePassword)
                    {
                        ConfirmPassword.SetValue(
                            TextFieldAssist.HintsProperty,
                            "Password does not match"
                        );
                    }
                    else
                        ConfirmPassword.ClearValue(TextFieldAssist.HintsProperty);
                }
            };

            Password.PropertyChanged += (sender, args) =>
            {
                if (args.Property == TextBox.TextProperty)
                {
                    if (!_content.IsSamePassword)
                    {
                        ConfirmPassword.SetValue(
                            TextFieldAssist.HintsProperty,
                            "Password does not match"
                        );
                    }
                    else
                        ConfirmPassword.ClearValue(TextFieldAssist.HintsProperty);
                }
            };
        }

        public UserView()
        {
            InitializeComponent();
            DataContext = _content;

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

                    _content.X = (int)window.Width / 2 + 320;
                    _content.Y = (int)window.Height / 2 - 100;

                    // Initial set
                    CreateUserForm.MinWidth = window.Width - 360;

                    // Subscribe to changes
                    window.PropertyChanged += updateMinWidth;
                }
            };

            Verification();
        }
    }
}
