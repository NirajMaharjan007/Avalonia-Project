using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MyApp.Services;
using ReactiveUI;

namespace MyApp.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private string _greet = "Hello, Avalonia!";
        private bool _isClick = false;

        public string Greeting
        {
            get => _greet;
            set => this.RaiseAndSetIfChanged(ref _greet, value);
        }

        public bool IsClick
        {
            get => _isClick;
            set => this.RaiseAndSetIfChanged(ref _isClick, value);
        }

        private void toggle()
        {
            IsClick = !IsClick;
        }

        public ICommand Click { get; }

        public MainViewModel()
        {
            Click = new RelayCommand(() =>
            {
                toggle();
                if (IsClick)
                    Greeting = "Button Clicked! " + Auth.UserId;
                else
                    Greeting = "Hello, Avalonia!";

                Console.WriteLine("Button Clicked! " + IsClick);
            });
        }
    }
}
