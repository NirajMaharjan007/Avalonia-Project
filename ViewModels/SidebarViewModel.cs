using System;
using MyApp.Views;
using ReactiveUI;

namespace MyApp.ViewModels
{
    public class SidebarViewModel : ReactiveObject
    {
        private RootView? _root;

        public RootView? Root
        {
            get => _root;
            set => this.RaiseAndSetIfChanged(ref _root, value);
        }

        private void Init()
        {
            Console.WriteLine("Holla");
        }

        public SidebarViewModel()
        {
            Init();
        }
    }
}
