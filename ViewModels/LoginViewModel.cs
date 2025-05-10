using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MyApp.Services;
using ReactiveUI;

namespace MyApp.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        private string _message = "N/A";
        private bool _error = false;
        private string _email = "";
        private string _password = "";

        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        public bool Error
        {
            get => _error;
            set => this.RaiseAndSetIfChanged(ref _error, value);
        }
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public ICommand ClickAction { get; }

        public LoginViewModel()
        {
            ClickAction = new RelayCommand(async () =>
            {
                if (await Auth.IsConnected())
                {
                    var flag = await Auth.LoginAsync(Email, Password);
                    if (flag)
                    {
                        Error = false;
                        Message = "Login Successfull";
                    }
                    else
                    {
                        Error = true;
                        Message = "Invalid credentials";
                    }
                }
                else
                {
                    Error = true;
                    Message = "Failed to connection";
                    Console.WriteLine("Failed to Connect database");
                }
            });
        }
    }
}
