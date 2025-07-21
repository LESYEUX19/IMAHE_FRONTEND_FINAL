using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Imahe; // Ensure this using statement is present

namespace Imahe
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
                });

            builder.Services.AddMauiBlazorWebView();

            // Register your HTTP client for dependency injection
            builder.Services.AddScoped(sp => new HttpClient());

            // ✅ CRITICAL: Register the AppState service as a SINGLETON
            builder.Services.AddSingleton<AppState>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}