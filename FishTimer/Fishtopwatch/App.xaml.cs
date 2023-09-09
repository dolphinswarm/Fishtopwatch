using Fishtopwatch.Data;

namespace Fishtopwatch
{
    public partial class App : Application
    {
        public static StopwatchRepository StopwatchRepository { get; private set; }

        public App(StopwatchRepository stopwatchRepository)
        {
            InitializeComponent();

            MainPage = new AppShell();

            StopwatchRepository = stopwatchRepository;
        }
    }
}