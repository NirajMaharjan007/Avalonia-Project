using System.Collections.Generic;
using MyApp.Services;
using ReactiveUI;

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

        public string Name => _name;
        public string Email => _email;
        public string Username => _username;
        public string CreatedOn => _created_on;

        private async void Fetch()
        {
            userDetail = await _user.GetUser(_id);
        }

        public SettingViewModel()
        {
            Fetch();
        }
    }
}
