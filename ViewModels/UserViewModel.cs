using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using MyApp.Services;
using ReactiveUI;

namespace MyApp.ViewModels
{
    public class UserViewModel : ReactiveObject
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }
        public ICommand ClickCommand { get; }

        public UserViewModel()
        {
            ClickCommand = new RelayCommand(() =>
            {
                Console.WriteLine("GG");
                var data = new UserData
                {
                    Username = Username,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                };
                Console.WriteLine("Hello\n");
                Console.WriteLine(data.Username + "This User");
                Console.WriteLine(data.FirstName + "This FN");
                Console.WriteLine(data.LastName + "This LN");
                Console.WriteLine(data.Email + "This EMail");
                Console.WriteLine(data.Password + "This Pass");
            });
        }
    }
}
