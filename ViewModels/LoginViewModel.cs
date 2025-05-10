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

        private bool _success = false;
        private string _username = "";
        private string _password = "";

        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        public bool Success
        {
            get => _success;
            set => this.RaiseAndSetIfChanged(ref _success, value);
        }
        public bool Error
        {
            get => _error;
            set => this.RaiseAndSetIfChanged(ref _error, value);
        }
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

        public ICommand ClickAction { get; }

        public event EventHandler? LoginSucceeded;

        public LoginViewModel()
        {
            ClickAction = new RelayCommand(async () =>
            {
                if (await Auth.IsConnected())
                {
                    var flag = await Auth.LoginAsync(Username, Password);
                    if (flag)
                    {
                        Success = true;
                        Error = false;
                        Message = "Login Successfull";
                        LoginSucceeded?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        Error = true;
                        Success = false;
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
