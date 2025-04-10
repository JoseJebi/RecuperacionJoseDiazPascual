using RecuperacionJoseDiazPascual.MVVM.Models;
using RecuperacionJoseDiazPascual.Repositories;

namespace RecuperacionJoseDiazPascual
{
    public partial class App : Application
    {
        public static BaseRepository<Tarea> TareaRepositorio { get; set; }
        public static BaseRepository<Etiqueta> EtiquetaRepositorio { get; set; }

        public App(BaseRepository<Tarea> objTareaRepo, BaseRepository<Etiqueta> objEtiquetaRepo)
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
