using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MyApp.Misc.Dialogs;
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

        public bool IsValidEmail
        {
            get
            {
                var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(Email, pattern, RegexOptions.IgnoreCase);
            }
        }

        public bool IsSamePassword => Password.Equals(ConfirmPassword);

        public bool Flag { get; private set; }
        public ICommand ClickCommand { get; }

        public UserViewModel()
        {
            ClickCommand = new RelayCommand(() =>
            {
                try
                {
                    Alert alert = new(Alert.Type.Success, "Successfully Created User");
                    Alert failed = new(Alert.Type.Error, "Failed to Created User");

                    // alert.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                /* Flag = IsValidEmail && IsSamePassword;
                var data = new UserData
                {
                    Username = Username,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                };


                if (Flag)
                {
                    bool tasks = await new User().CreateUser(data);
                    if (tasks)
                    {
                        Console.WriteLine("Flag " + Flag + " DONE");
                    }
                    else
                    {
                        Console.WriteLine("Flag " + Flag + " Failed");
                    }
                }
                else
                {
                    Console.WriteLine("Flag " + Flag);
                } */
            });
        }
    }
}
