using Microsoft.Extensions.Logging;
using RecuperacionJoseDiazPascual.MVVM.Models;

namespace RecuperacionJoseDiazPascual
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
                });

            builder.Services.AddSingleton<Repositories.BaseRepository<Tarea>>();
            builder.Services.AddSingleton<Repositories.BaseRepository<Etiqueta>>();
            builder.Services.AddSingleton<Repositories.BaseRepository<EtiquetasTarea>>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
