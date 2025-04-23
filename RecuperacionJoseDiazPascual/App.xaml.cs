using RecuperacionJoseDiazPascual.MVVM.Models;
using RecuperacionJoseDiazPascual.MVVM.Views;
using RecuperacionJoseDiazPascual.Repositories;

namespace RecuperacionJoseDiazPascual
{
    public partial class App : Application
    {
        public static BaseRepository<Tarea> TareaRepositorio { get; set; }
        public static BaseRepository<Etiqueta> EtiquetaRepositorio { get; set; }
        public static BaseRepository<EtiquetasTarea> EtiquetasTareaRepositorio { get; set; }


        public App(BaseRepository<Tarea> objTareaRepo, BaseRepository<Etiqueta> objEtiquetaRepo, 
            BaseRepository<EtiquetasTarea> objEtiquetasTareaRepo)
        {
            InitializeComponent();

            TareaRepositorio = objTareaRepo;
            EtiquetaRepositorio = objEtiquetaRepo;
            EtiquetasTareaRepositorio = objEtiquetasTareaRepo;

            MainPage = new NavigationPage(new PrincipalView());
            //MainPage = new GestionEtiquetasView();
        }
    }
}
