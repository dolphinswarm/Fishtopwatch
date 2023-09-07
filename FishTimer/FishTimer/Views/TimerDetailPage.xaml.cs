using FishTimer.Models;

namespace FishTimer.Views;

[QueryProperty(nameof(Timer), "Timer")]
public partial class TimerDetailPage : ContentPage
{
    private const int TIMER_DURATION_IN_MILLISECONDS = 1000;

    TimerModel timer;
    public TimerModel Timer
    {
        get => timer;
        set
        {
            timer = value;
            OnPropertyChanged();

            SetViewInitialTime(value);

            // If the timer WAS running, then show resume text. Otherwise, show stop text
            ToggleTimerButton.Text = $"{(!value.IsRunning ? "Resume" : "Stop")} timer";
            SemanticProperties.SetHint(ToggleTimerButton, $"{(!value.IsRunning ? "Resumes" : "Stops")} the timer");
        }
    }

    TimeSpan currentTime;
    public TimeSpan CurrentTime
    {
        get => currentTime;
        set
        {
            currentTime = value;
            OnPropertyChanged();
        }
    }

    Timer _internalTimer;
    
    public TimerDetailPage()
	{
        InitializeComponent();
        BindingContext = this;

        // Start the internal timer
        _internalTimer = new Timer(
            new TimerCallback(TimerTick),
            null,
            TIMER_DURATION_IN_MILLISECONDS,
            TIMER_DURATION_IN_MILLISECONDS);
    }

    async void TimerTick(object stateInfo)
    {
        if (Timer.IsRunning)
        {
            Timer.ElapsedTime++;
            CurrentTime += TimeSpan.FromSeconds(1);

            await App.TimerRepository.UpdateTimer(Timer);
        }
    }

    void SetViewInitialTime(TimerModel timer)
    {
        // If the timer is running, get the most recent start time and get the number of seconds between them
        uint secondsSinceLastStart = timer.IsRunning ?
            (uint)(DateTime.Now - DateTime.Parse(timer.MostRecentStartTime)).TotalSeconds :
            0; // <-- Else, just return 0 since ther is no change in time

        // Get the number of seconds and create a timespan out of it
        CurrentTime = TimeSpan.FromSeconds(timer.ElapsedTime + secondsSinceLastStart);
    }

    async Task ToggleTimer()
    {
        var wasTimeRunning = Timer.IsRunning;

        Timer.IsRunning = !wasTimeRunning;  // <-- Invert the "Running" flag
        Timer.MostRecentStartTime = wasTimeRunning ? Timer.MostRecentStartTime : DateTime.Now.ToString(); // <-- Set the most recent start time if the timer was just started

        // Send the new timer to the database
        await App.TimerRepository.UpdateTimer(Timer);

        // If the timer WAS running, then show resume text. Otherwise, show stop text
        ToggleTimerButton.Text = $"{(wasTimeRunning ? "Resume" : "Stop")} timer";
        SemanticProperties.SetHint(ToggleTimerButton, $"{(wasTimeRunning ? "Resumes" : "Stops")} the timer");
    }

    private async void OnToggleTimerButtonClicked(object sender, EventArgs e)
    {
        await ToggleTimer();
    }
}