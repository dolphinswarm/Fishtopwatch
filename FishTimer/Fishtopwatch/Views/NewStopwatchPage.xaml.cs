using Fishtopwatch;
using Fishtopwatch.Models;

namespace Fishtopwatch.Views
{
	public partial class NewStopwatchPage : ContentPage
	{
		public NewStopwatchPage()
		{
			InitializeComponent();
		}
		private async void OnCreateStopwatchButtonClicked(object sender, EventArgs e)
		{
			// Create a new Stopwatch Model from the form
			var newStopwatch = new StopwatchModel
			{
				Name = NewStopwatchNameField.Text,
				Description = NewStopwatchDescriptionField.Text,
				StartTime = DateTime.Now.ToString(),
				MostRecentStartTime = DateTime.Now.ToString(),
			};

			// Add it to the database
			var result = await App.StopwatchRepository.AddStopwatch(newStopwatch);

			// Display an alert based on the result
			await DisplayAlert(
				result ? "Success!" : "Failure!",
				result ? $"The Stopwatch {newStopwatch.Name} was successfully started!" : $"Something went wrong trying to start the new Stopwatch!",
				"OK"
			);

			// Redirect back to home if this Stopwatch got made
			if (result)
				await Shell.Current.GoToAsync("..");
		}

	}
}