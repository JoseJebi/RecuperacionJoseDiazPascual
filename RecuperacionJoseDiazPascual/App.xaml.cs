using RecuperacionJoseDiazPascual.MVVM.Models;
using RecuperacionJoseDiazPascual.MVVM.Views;
using RecuperacionJoseDiazPascual.Repositories;

namespace RecuperacionJoseDiazPascual
{
    public partial class App : Application
    {
        public static BaseRepository<Tarea> TareaRepositorio { get; set; }

        public App(BaseRepository<Tarea> objTareaRepo)
        {
            InitializeComponent();

            TareaRepositorio = objTareaRepo;

            MainPage = new PrincipalView();
        }
    }
}
