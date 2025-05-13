using System;
using MyApp.Services;
using ReactiveUI;

namespace MyApp.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private int _userCount = 0;
        private int _activeUserCount = 0;

        private readonly User user;
        public int UserCount
        {
            get => _userCount;
            set => this.RaiseAndSetIfChanged(ref _userCount, value);
        }
        public int ActiveUserCount
        {
            get => _activeUserCount;
            set => this.RaiseAndSetIfChanged(ref _activeUserCount, value);
        }

        private async void Init()
        {
            UserCount = await user.GetUserCount();
            ActiveUserCount = await user.GetActiveUserCount();
        }

        public MainViewModel()
        {
            user = new();

            Init();
        }
    }
}
