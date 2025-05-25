using System;
using System.Threading.Tasks;
using MyApp.Services;
using ReactiveUI;
using static System.Console;

namespace MyApp.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private int _userCount = 0;
        private int _activeUserCount = 0;

        private bool _loading = false;
        private bool _visible = false;

        private readonly User user = new();
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

        public bool Loading
        {
            get => _loading;
            set => this.RaiseAndSetIfChanged(ref _loading, value);
        }

        public bool Visiblity
        {
            get => _visible;
            set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        private async void Init()
        {
            WriteLine("Load " + Loading + " Visiblity" + Visiblity);
            Loading = true;
            Visiblity = false;
            await Task.Delay(3200);
            UserCount = await user.GetUserCount();
            ActiveUserCount = await user.GetActiveUserCount();
            Loading = false;
            Visiblity = true;
        }

        public MainViewModel()
        {
            Init();
        }
    }
}
