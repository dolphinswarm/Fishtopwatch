using Fishtopwatch;
using Fishtopwatch.Models;

namespace Fishtopwatch.Views
{
	public partial class NewStopwatchPage : ContentPage
	{
		//Color pickedColor;

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
				//Color = pickedColor.ToHex()
            };

			// Add it to the database
			var result = await App.StopwatchRepository.AddStopwatch(newStopwatch);

			// Display an alert based on the result
			await DisplayAlert(
				result ? "Success!" : "Failure!",
				result ? $"The stopwatch {newStopwatch.Name} was successfully started!" : "Something went wrong trying to start the new stopwatch!",
				"OK"
			);

			// Redirect back to home if this Stopwatch got made
			if (result)
				await Shell.Current.GoToAsync("..");
		}

		// TODO LOOK INTO COLOR PICKING AND BINDING
   //     private void OnPickedColorChanged(object sender, Color colorPicked)
   //     {
   //         // Use the selected color
   //         SelectedColorValueLabel.Text = colorPicked.ToHex();
   //         SelectedColorValueLabel.Background = colorPicked;
			//pickedColor = colorPicked;
   //     }
    }
}