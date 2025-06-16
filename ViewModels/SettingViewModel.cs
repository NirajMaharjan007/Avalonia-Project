using System;
using MyApp.Services;
using ReactiveUI;
using static System.Console;
using static System.Globalization.DateTimeStyles;

namespace MyApp.ViewModels
{
    public class SettingViewModel : ReactiveObject
    {
        public UserData userDetail = new();

        private int _id = Auth.UserId;

        private string _name = string.Empty,
            _email = string.Empty,
            _username = string.Empty,
            _created_on = string.Empty;
        private User _user = new();

        public string Name
        {
            set => this.RaiseAndSetIfChanged(ref _name, value);
            get => _name;
        }
        public string Email
        {
            set => this.RaiseAndSetIfChanged(ref _email, value);
            get => _email;
        }
        public string Username
        {
            set => this.RaiseAndSetIfChanged(ref _username, value);
            get => _username;
        }
        public string CreatedOn
        {
            set => this.RaiseAndSetIfChanged(ref _created_on, value);
            get => _created_on;
        }

        public int Id => _id;

        private string FullDateTime(string isoTimestamp)
        {
            DateTime dateTime = DateTime.Parse(isoTimestamp, null, RoundtripKind);
            return dateTime.ToString("yyyy/MMMM/dd HH:mm:ss");
        }

        private async void Fetch()
        {
            userDetail = await _user.GetUser(_id);
            Name =
                string.IsNullOrWhiteSpace(userDetail.FirstName)
                && string.IsNullOrWhiteSpace(userDetail.LastName)
                    ? "N/A"
                    : $"{userDetail.FirstName} {userDetail.LastName}".Trim();
            Email = userDetail.Email + "";
            Username = userDetail.Username + "";
            CreatedOn = FullDateTime(userDetail.CreatedOn + "");

            WriteLine($"these r {Id} {Name} {Email} {CreatedOn}");
        }

        public SettingViewModel()
        {
            Fetch();
        }
    }
}
