using System;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MyApp.Services;
using ReactiveUI;

namespace MyApp.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        private Action? _action;

        private Auth _auth = new();

        private string _message = "N/A";
        private bool _error = false;

        private bool _rememberMe;

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

        public bool RememberMe
        {
            get => _rememberMe;
            set => this.RaiseAndSetIfChanged(ref _rememberMe, value);
        }

        public ICommand ClickAction { get; }

        public ICommand EnterAction { get; }

        public ICommand RememberAction { get; }
        public Action? Action
        {
            get => _action;
            set => _action = value;
        }

        public event EventHandler? LoginSucceeded;

        public LoginViewModel()
        {
            TryAutoLogin();

            Action = async () =>
            {
                if (await _auth.IsConnected())
                {
                    var flag = await _auth.LoginAsync(Username, Password);

                    if (flag)
                    {
                        Console.WriteLine("Flag " + flag + " Remember Me " + RememberMe);
                        if (RememberMe)
                            SaveCredentials();
                        else
                            ClearSavedCredentials();

                        Error = false;
                        LoginSucceeded?.Invoke(this, EventArgs.Empty);
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
            };

            ClickAction = new RelayCommand(Action);
            EnterAction = new RelayCommand(Action);
            RememberAction = new RelayCommand(() =>
            {
                RememberMe = !_rememberMe;
            });
        }

        private void SaveCredentials()
        {
            string data = Username + "->" + Password;
            File.WriteAllText("remember.log", data);
        }

        private void ClearSavedCredentials()
        {
            if (File.Exists("remember.log"))
                File.Delete("remember.log");
        }

        private void TryAutoLogin()
        {
            if (File.Exists("remember.log"))
            {
                string data = File.ReadAllText("remember.log");
                string[] credentials = data.Split("->");
                Username = credentials[0];
                Password = credentials[1];
                RememberMe = true;
            }
        }
    }
}
