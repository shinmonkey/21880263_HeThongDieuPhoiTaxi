using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Maps;
using CustomerApp.Hubs;
using MauiIcons.Material;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Maps;

namespace CustomerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMaterialMauiIcons()
                .UseMauiCommunityToolkit()
                .UseMauiMaps()
                .UseMauiCommunityToolkitMaps("ArXuXGJK-R5u5jPJdd7Lo2QQbjDwqc2KgT0diZpp8jWhv-jpcpmSDrqZvUdN3fUo")
                .ConfigureEssentials(essentials =>
                    {
                        essentials.UseMapServiceToken("ArXuXGJK-R5u5jPJdd7Lo2QQbjDwqc2KgT0diZpp8jWhv-jpcpmSDrqZvUdN3fUo");
                    });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}