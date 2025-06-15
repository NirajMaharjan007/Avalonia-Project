using Avalonia.Controls;
using MyApp.ViewModels;

namespace MyApp.Views.Subviews
{
    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();

            DataContext = new SettingViewModel();
        }
    }
}
