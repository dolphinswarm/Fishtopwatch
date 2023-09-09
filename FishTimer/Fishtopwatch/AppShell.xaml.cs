using Fishtopwatch.Views;

namespace Fishtopwatch
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("stopwatches/details", typeof(StopwatchDetailPage));
            Routing.RegisterRoute("stopwatches/new", typeof(NewStopwatchPage));
        }
    }
}