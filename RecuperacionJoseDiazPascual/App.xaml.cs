using RecuperacionJoseDiazPascual.MVVM.Views;

namespace RecuperacionJoseDiazPascual
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AgregarView();
        }
    }
}
