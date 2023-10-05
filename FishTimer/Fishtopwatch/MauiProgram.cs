using CommunityToolkit.Maui;
using Fishtopwatch.Data;
using Fishtopwatch.ViewModels;
using Fishtopwatch.Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Fishtopwatch
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(s => 
                ActivatorUtilities.CreateInstance<StopwatchRepository>(s));
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<StopwatchViewModel>();

            #if DEBUG
		        builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
