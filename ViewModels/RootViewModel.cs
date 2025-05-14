using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace MyApp.ViewModels
{
    public class RootViewModel : ReactiveObject
    {
        private bool _expand = true;
        public bool Expand
        {
            get => _expand;
            set => this.RaiseAndSetIfChanged(ref _expand, value);
        }

        private void Toggle()
        {
            Expand = !_expand;
        }

        public ICommand ClickAction { get; }

        public RootViewModel()
        {
            ClickAction = new RelayCommand(() =>
            {
                Toggle();
            });
        }
    }
}
