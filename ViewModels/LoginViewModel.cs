using System;
using System.Windows.Input;
using Avalonia.Data;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using MyApp.Services;
using ReactiveUI;

namespace MyApp.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        private Action? _action;

        private string _message = "N/A";
        private bool _error = false;

        private bool _success = false;
        private string _username = string.Empty;
        private string _usernameHint = string.Empty;

        private string _password = string.Empty;

        private string _passwordHint = string.Empty;
        public string PasswordHint
        {
            get => _passwordHint;
            set => this.RaiseAndSetIfChanged(ref _passwordHint, value);
        }

        public string UsernameHint
        {
            get => _usernameHint;
            set => this.RaiseAndSetIfChanged(ref _usernameHint, value);
        }

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
            set
            {
                this.RaiseAndSetIfChanged(ref _username, value);
                UsernameHint = string.IsNullOrWhiteSpace(value)
                    ? "Username field may be empty"
                    : "";
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                this.RaiseAndSetIfChanged(ref _password, value);
                PasswordHint = string.IsNullOrWhiteSpace(value)
                    ? "Password field may be empty"
                    : "";
            }
        }

        public ICommand ClickAction { get; }

        public ICommand EnterAction { get; }
        public Action? Action
        {
            get => _action;
            set => _action = value;
        }

        public event EventHandler? LoginSucceeded;

        public LoginViewModel()
        {
            Action = async () =>
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
            };

            ClickAction = new RelayCommand(Action);
            EnterAction = new RelayCommand(Action);
        }
    }
}
