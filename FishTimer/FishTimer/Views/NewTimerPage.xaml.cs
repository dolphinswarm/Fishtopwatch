using FishTimer.Data;
using FishTimer.Models;

namespace FishTimer.Views;

public partial class NewTimerPage : ContentPage
{
	public NewTimerPage()
	{
		InitializeComponent();
	}
    private async void OnCreateTimerButtonClicked(object sender, EventArgs e)
    {
		// Create a new Timer Model from the form
		var newTimer = new TimerModel
		{
			Name = NewTimerNameField.Text,
			Description = NewTimerDescriptionField.Text,
			StartTime = DateTime.Now.ToString()
		};

		// Add it to the database
		var result = await App.TimerRepository.AddTimer(newTimer);

		// Display an alert based on the result
        await DisplayAlert(
			result ? "Success!" : "Failure!",
			result ? $"The timer {newTimer.Name} was successfully started!" : $"Something went wrong trying to start the new timer!",
			"OK"
		);

		// Redirect back to home if this timer got made
		if (result)
			await Shell.Current.GoToAsync("..");
    }

}