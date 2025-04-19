using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;

namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GestionEtiquetasViewModel
    {
        public ICommand VolverAgregarTarea { get; }
        public GestionEtiquetasViewModel() 
        {
            VolverAgregarTarea = new Command(Volver);
        }

        public async void Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
