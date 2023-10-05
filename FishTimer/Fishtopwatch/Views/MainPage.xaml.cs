using CommunityToolkit.Maui.Behaviors;
using Fishtopwatch.Animations;
using Fishtopwatch.Models;
using Fishtopwatch.ViewModels;

namespace Fishtopwatch.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(StopwatchViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }

        private async void OnItemClicked(object sender, SelectionChangedEventArgs e)
        {
            var stopwatch = e.CurrentSelection.FirstOrDefault() as StopwatchModel;

            await Shell.Current.GoToAsync($"stopwatches/details", new Dictionary<string, object> {
                { "Stopwatch", stopwatch }
            });
        }

        private async void OnCreateStopwatchButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("stopwatches/new");
        }
        
        private async void OnDeleteStopwatchsButtonClicked(object sender, EventArgs e)
        {
            await App.StopwatchRepository.DeleteAllStopwatchs();
        }
    }
}