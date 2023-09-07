using FishTimer.Models;
using FishTimer.ViewModels;

namespace FishTimer
{
    public partial class MainPage : ContentPage
    {
        public MainPage(TimerViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }

        private async void OnItemClicked(object sender, SelectionChangedEventArgs e)
        {
            var timer = e.CurrentSelection.FirstOrDefault() as TimerModel;

            await Shell.Current.GoToAsync($"timers/details", new Dictionary<string, object> {
                { "Timer", timer }
            });
        }

        private async void OnCreateTimerButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("timers/new");
        }
    }
}