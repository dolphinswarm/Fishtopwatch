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
            bool shouldDeleteForReal = await DisplayAlert("Are you sure?", "Are you sure you'd like to delete this stopwatch? This action is irreversible!", "Yes", "No");

            if (shouldDeleteForReal)
            {
                // Delete all items from the db
                await App.StopwatchRepository.DeleteAllStopwatches();

                // Execute the command to fetch the data after deleteing the items
                var vm = (StopwatchViewModel)BindingContext;
                if (vm.GetStopwatchDataCommand.CanExecute(null))
                {
                    vm.GetStopwatchDataCommand.Execute(null);
                }
            }
        }
    }
}