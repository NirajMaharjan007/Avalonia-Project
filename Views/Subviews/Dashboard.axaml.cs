using Avalonia.Controls;
using MyApp.ViewModels;

namespace MyApp.Views.Subviews
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
