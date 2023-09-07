using FishTimer.Views;

namespace FishTimer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("timers/details", typeof(TimerDetailPage));
            Routing.RegisterRoute("timers/new", typeof(NewTimerPage));
        }
    }
}