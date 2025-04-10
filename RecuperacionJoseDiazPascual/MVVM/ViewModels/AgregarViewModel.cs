using PropertyChanged;
using RecuperacionJoseDiazPascual.MVVM.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AgregarViewModel
    {
        public ICommand AgTarea { get; }
        public string AgTitulo { get; set; }
        public string AgDescripcion { get; set; }
        public string PrioridadSeleccionada { get; set; }
        public bool Estado { get; set; }

        public List<string> ListaPrioridades { get; set; }

        public AgregarViewModel()
        {
            ListaPrioridades = new List<string> { "Alta", "Media", "Baja" };
            PrioridadSeleccionada = ListaPrioridades[1];

            AgTarea = new Command(GuardarTarea);
        }

        // Método para guardar la tarea nueva
        private void GuardarTarea()
        {
           
            var nuevaTarea = new Tarea
            {
                Titulo = AgTitulo,
                Descripcion = AgDescripcion,
                Prioridad = PrioridadSeleccionada,
                Estado = Estado ? "Finalizada" : "Pendiente"
            };

            App.TareaRepositorio.SaveItem(nuevaTarea);

            // Vuelve atrás después de guardar
            //Application.Current.MainPage.Navigation.PopAsync();
        }











        /*
            1. Para el check de tarea finalizada o no utilizar un convertes como en los apuntes.
            2. Para crear comandos T8_07 pag.7 [Solamente para botones]
            3. Para el CRUD T8_13 pag.6 
        */
    }
}
