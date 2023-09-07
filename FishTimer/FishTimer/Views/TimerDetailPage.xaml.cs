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

            SetCurrentTime(value);
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

    void TimerTick(object stateInfo)
    {
        SetCurrentTime(Timer);
    }

    void SetCurrentTime(TimerModel timer)
    {
        // Use the end time if we have it, otherwise the timer is running so just set it to now
        // The ToString is hacky (should just do a proper ternary or TryParse) but this works sooooo
        CurrentTime = DateTime.Parse(timer.EndTime ?? DateTime.Now.ToString()) - DateTime.Parse(Timer.StartTime);
    }

    async Task ToggleTimer()
    {
        // Is the timer running?
        var isTimerRunning = Timer.EndTime == null;

        // If it is, stop it by adding an end time. Else, restart it by deleting it
        Timer.EndTime = isTimerRunning ? DateTime.Now.ToString() : null;

        // Send the new timer to the database
        await App.TimerRepository.UpdateTimer(Timer);

        // If the timer WAS running, then show resume text. Otherwise, show stop text
        ToggleTimerButton.Text = $"{(isTimerRunning ? "Resume" : "Stop")} timer";
        SemanticProperties.SetHint(ToggleTimerButton, $"{(isTimerRunning ? "Resumes" : "Stops")} the timer");
    }

    private async void OnToggleTimerButtonClicked(object sender, EventArgs e)
    {
        await ToggleTimer();
    }
}