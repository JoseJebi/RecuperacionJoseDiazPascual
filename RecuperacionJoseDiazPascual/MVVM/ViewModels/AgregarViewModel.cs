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
        public List<string> ListaPrioridades { get; set; }
        public string PrioridadSeleccionada { get; set; }
        public List<string> ListaEtiquetas { get; set; }
        public List<string> EtiquetasSeleccionadas { get; set; }

        public bool Estado { get; set; }

        public string EtiquetaTemporal
        {
            get => null; // Siempre retorna null para resetear el Picker
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !EtiquetasSeleccionadas.Contains(value))
                {
                    EtiquetasSeleccionadas.Add(value);
                }
            }
        }

        public string EtiquetasSeleccionadasString =>
            EtiquetasSeleccionadas.Any() ? string.Join(", ", EtiquetasSeleccionadas) : "Ninguna etiqueta seleccionada";

        private void EliminarEtiqueta(string etiqueta)
        {
            EtiquetasSeleccionadas.Remove(etiqueta);
        }

        public AgregarViewModel()
        {
            ListaPrioridades = new List<string> { "Alta", "Media", "Baja" };
            PrioridadSeleccionada = ListaPrioridades[1];

            ListaEtiquetas = new List<string> { "Trabajo", "Estudios", "Personal", "Salud" };
            EtiquetasSeleccionadas = new List<string>();

            AgTarea = new Command(GuardarTarea);
        }



        // Método para guardar la tarea nueva
        private async void GuardarTarea()
        {
            if (string.IsNullOrWhiteSpace(AgTitulo) ||
                string.IsNullOrWhiteSpace(AgDescripcion) ||
                PrioridadSeleccionada == null ||
                EtiquetasSeleccionadas == null || !EtiquetasSeleccionadas.Any())
            {
                await Shell.Current.DisplayAlert("Campos incompletos", "Por favor, completa todos los campos antes de continuar.", "Aceptar");
                return;
            }
                
            var nuevaTarea = new Tarea
            {
                Titulo = AgTitulo,
                Descripcion = AgDescripcion,
                Prioridad = PrioridadSeleccionada,
                Estado = Estado ? "Finalizada" : "Pendiente",
                Etiquetas = EtiquetasSeleccionadas.Select(titulo => new Etiqueta { Titulo = titulo }).ToList()
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
