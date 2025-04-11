using PropertyChanged;
using RecuperacionJoseDiazPascual.MVVM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AgregarViewModel
    {
        public ICommand AgTarea { get; }
        public string? AgTitulo { get; set; }
        public string? AgDescripcion { get; set; }
        public bool Estado { get; set; }

        public List<string> ListaPrioridades { get; set; }
        public string PrioridadSeleccionada { get; set; }


        public List<string> ListaEtiquetas { get; set; }
        public List<string> EtiquetasSeleccionadas { get; set; }
        public ICommand LimpiarEtiquetasCommand { get; }
        public string EtiquetaTemporal
        {
            get => null;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !EtiquetasSeleccionadas.Contains(value))
                {
                    EtiquetasSeleccionadas.Add(value);
                }
            }
        }
        public string EtiquetasSeleccionadasString =>
            EtiquetasSeleccionadas.Any()
                ? string.Join(", ", EtiquetasSeleccionadas)
                : "Ninguna etiqueta seleccionada";

        // Constructor
        public AgregarViewModel()
        {
            ListaPrioridades = new List<string> { "Alta", "Media", "Baja" };
            PrioridadSeleccionada = ListaPrioridades[1];

            ListaEtiquetas = new List<string> { "Trabajo", "Estudios", "Personal", "Salud" };
            EtiquetasSeleccionadas = new List<string>();

            LimpiarEtiquetasCommand = new Command(() => EtiquetasSeleccionadas.Clear());

            AgTarea = new Command(GuardarTarea);
        }

        // Método para guardar la tarea creada
        private async void GuardarTarea()
        {
            if (string.IsNullOrWhiteSpace(AgTitulo) ||
                string.IsNullOrWhiteSpace(AgDescripcion) ||
                PrioridadSeleccionada == null ||
                !EtiquetasSeleccionadas.Any())
            {
                await Shell.Current.DisplayAlert("Error", "Por favor, completa todos los campos", "Aceptar");
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
        }
    }
}