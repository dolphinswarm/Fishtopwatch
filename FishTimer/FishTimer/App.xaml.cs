using FishTimer.Data;

namespace FishTimer
{
    public partial class App : Application
    {
        public static TimerRepository TimerRepository { get; private set; }

        public App(TimerRepository timerRepository)
        {
            InitializeComponent();

            MainPage = new AppShell();

            TimerRepository = timerRepository;
        }
    }
}