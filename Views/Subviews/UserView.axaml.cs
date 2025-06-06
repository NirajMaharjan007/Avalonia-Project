using System.Text.RegularExpressions;
using Avalonia.Controls;
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

            EmailBox.TextChanged += (_, __) =>
            {
                // UserBox.Text = EmailBox.Text?.Split('@')[0];

                UserBox.Text = MyRegex().Replace(EmailBox.Text?.Split('@')[0] ?? "", "");
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

            Verification();
        }

        [GeneratedRegex(@"[^a-zA-Z0-9]")]
        private static partial Regex MyRegex();
    }
}
