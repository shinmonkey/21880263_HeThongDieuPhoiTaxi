using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Maps;
using MauiIcons.Material;
using Microsoft.Extensions.Logging;

namespace DriverApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMaterialMauiIcons()
                .UseMauiMaps()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMaps("Au-DVolT8rjOmJOlFmxYl4pLQTL00UgPwWHOAj53tq3cXLQS3mo2WS9XdI4pVly-")
                .ConfigureEssentials(essentials =>
                {
                    essentials.UseMapServiceToken("ArXuXGJK-R5u5jPJdd7Lo2QQbjDwqc2KgT0diZpp8jWhv-jpcpmSDrqZvUdN3fUo");
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}