using System;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MyApp.Misc.Dialogs;
using MyApp.Views.Subviews;
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

        private UserControl? _currentView;
        public UserControl? CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        private object? _selectedMenuItem;
        public object? SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMenuItem, value);
                OnMenuItemSelected(value as ListBoxItem);
            }
        }

        private void Toggle()
        {
            Expand = !_expand;
        }

        public ICommand ClickAction { get; }

        public RootViewModel()
        {
            Console.WriteLine("ITEM", _selectedMenuItem);
            CurrentView = new Dashboard();

            ClickAction = new RelayCommand(() =>
            {
                Toggle();
            });
        }

        private void OnMenuItemSelected(ListBoxItem? item)
        {
            switch (item?.Tag)
            {
                case "Dashboard":
                    CurrentView = new Dashboard();
                    break;

                case "Users":
                    CurrentView = new UserView();
                    break;

                case "Settings":
                    CurrentView = new SettingView();
                    break;

                case "Logout":
                    // CurrentView = new Dashboard();
                    _ = new Alert(Alert.Type.Warning, "Are sure about to logout?")
                    {
                        Confirmation = true,
                        Borderless = true,
                    };

                    // alert.Show();
                    break;
            }
            Console.WriteLine("item>" + item?.Tag);
        }
    }
}
