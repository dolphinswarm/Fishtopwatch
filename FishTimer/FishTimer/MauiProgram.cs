using CommunityToolkit.Maui;
using FishTimer.Data;
using FishTimer.ViewModels;
using Microsoft.Extensions.Logging;

namespace FishTimer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(s => 
                ActivatorUtilities.CreateInstance<TimerRepository>(s));
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<TimerViewModel>();

            #if DEBUG
		        builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
