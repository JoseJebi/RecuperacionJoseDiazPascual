using RecuperacionJoseDiazPascual.MVVM.Models;
using RecuperacionJoseDiazPascual.MVVM.Views;
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

            TareaRepositorio = objTareaRepo;

            EtiquetaRepositorio = objEtiquetaRepo;

            MainPage = new PrincipalView();
        }
    }
}
