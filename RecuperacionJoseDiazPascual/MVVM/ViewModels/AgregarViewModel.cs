using PropertyChanged;
using System.Collections.Generic;

namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AgregarViewModel
    {
        public string AgTarea { get; set; }
        public string AgDescripcion { get; set; }
        public string PrioridadSeleccionada { get; set; }
        public bool Estado { get; set; }

        public List<string> ListaPrioridades { get; set; }

        public AgregarViewModel()
        {
            ListaPrioridades = new List<string> { "Alta", "Media", "Baja" };
            PrioridadSeleccionada = ListaPrioridades[1];
        }

        /*
            1. Para el check de tarea finalizada o no utilizar un convertes como en los apuntes.
            2. Para crear comandos T8_07 pag.7 [Solamente para botones]
            3. Para el CRUD T8_13 pag.6 
        */
    }
}
