using Fishtopwatch.Animations;
using Fishtopwatch.Models;

namespace Fishtopwatch.Views
{

    [QueryProperty(nameof(Stopwatch), "Stopwatch")]
    public partial class StopwatchDetailPage : ContentPage
    {
        private const int TIMER_DURATION_IN_MILLISECONDS = 1000;

        StopwatchModel stopwatch;
        public StopwatchModel Stopwatch
        {
            get => stopwatch;
            set
            {
                stopwatch = value;
                OnPropertyChanged();

                SetViewInitialTime(value);

                // If the stopwatch WAS running, then show resume text. Otherwise, show stop text
                ToggleStopwatchButton.Text = $"{(!value.IsRunning ? "Resume" : "Stop")} stopwatch";
                SemanticProperties.SetHint(ToggleStopwatchButton, $"{(!value.IsRunning ? "Resumes" : "Stops")} the stopwatch");
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

        public StopwatchDetailPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Start the internal stopwatch
            _internalTimer = new Timer(
                new TimerCallback(StopwatchTick),
                null,
                TIMER_DURATION_IN_MILLISECONDS,
                TIMER_DURATION_IN_MILLISECONDS);
        }

        void StopwatchTick(object stateInfo)
        {
            if (Stopwatch.IsRunning)
            {
                CurrentTime += TimeSpan.FromSeconds(1);
            }
        }

        void SetViewInitialTime(StopwatchModel stopwatch)
        {
            // If the stopwatch is running, get the most recent start time and get the number of seconds between them
            uint secondsSinceLastStart = stopwatch.IsRunning ?
                (uint)(DateTime.Now - DateTime.Parse(stopwatch.MostRecentStartTime)).TotalSeconds :
                0; // <-- Else, just return 0 since ther is no change in time

            // Get the number of seconds and create a timespan out of it
            CurrentTime = TimeSpan.FromSeconds(stopwatch.ElapsedTime + secondsSinceLastStart);
        }

        async Task ToggleStopwatch()
        {
            var wasTimeRunning = Stopwatch.IsRunning;

            Stopwatch.IsRunning = !wasTimeRunning;  // <-- Invert the "Running" flag
            Stopwatch.ElapsedTime += wasTimeRunning ? (uint)(DateTime.Now - DateTime.Parse(Stopwatch.MostRecentStartTime)).TotalSeconds : 0; // Add time when the stopwatch stops
            Stopwatch.MostRecentStartTime = wasTimeRunning ? Stopwatch.MostRecentStartTime : DateTime.Now.ToString(); // <-- Set the most recent start time if the stopwatch was just started

            // Send the new stopwatch to the database
            await App.StopwatchRepository.UpdateStopwatch(Stopwatch);

            // If the stopwatch WAS running, then show resume text. Otherwise, show stop text
            ToggleStopwatchButton.Text = $"{(wasTimeRunning ? "Resume" : "Stop")} stopwatch";
            SemanticProperties.SetHint(ToggleStopwatchButton, $"{(wasTimeRunning ? "Resumes" : "Stops")} the stopwatch");
        }

        private async void OnToggleStopwatchButtonClicked(object sender, EventArgs e)
        {
            await ToggleStopwatch();
        }
    }
}