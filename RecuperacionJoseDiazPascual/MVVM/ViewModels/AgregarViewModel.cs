using PropertyChanged;
using RecuperacionJoseDiazPascual.MVVM.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AgregarViewModel
    {

        public ICommand AgTarea { get; }
        public ICommand VolverPaginaPrincipal { get; }
        public string? AgTitulo { get; set; }
        public string? AgDescripcion { get; set; }
        public bool Estado { get; set; }

        public List<string> ListaPrioridades { get; set; }
        public string PrioridadSeleccionada { get; set; }

        // Cambiamos a ObservableCollection para que la UI se actualice automáticamente
        public ObservableCollection<string> ListaEtiquetas { get; set; }

        // Modelo para manejar las etiquetas seleccionadas
        public ObservableCollection<EtiquetaSeleccionada> EtiquetasConSeleccion { get; set; }

        public ICommand LimpiarEtiquetasCommand { get; }
        private Tarea tareaEditando;

        // Propiedad calculada para mostrar las etiquetas seleccionadas
        public string EtiquetasSeleccionadasString =>
            EtiquetasConSeleccion.Any(e => e.Seleccionada)
                ? string.Join(", ", EtiquetasConSeleccion.Where(e => e.Seleccionada).Select(e => e.Nombre))
                : "Ninguna etiqueta seleccionada";

        public AgregarViewModel()
        {
            ListaPrioridades = new List<string> { "Alta", "Media", "Baja" };
            PrioridadSeleccionada = ListaPrioridades[1];

            // Inicializamos la lista de etiquetas con objetos que tienen estado de selección
            EtiquetasConSeleccion = new ObservableCollection<EtiquetaSeleccionada>(
                new List<string> { "Trabajo", "Estudios", "Personal", "Salud" }
                    .Select(e => new EtiquetaSeleccionada { Nombre = e, Seleccionada = false })
            );

            // Comando para limpiar selecciones
            LimpiarEtiquetasCommand = new Command(() =>
            {
                foreach (var etiqueta in EtiquetasConSeleccion)
                {
                    etiqueta.Seleccionada = false;
                }
            });

            AgTarea = new Command(GuardarTarea);
            VolverPaginaPrincipal = new Command(Volver);
        }

        public AgregarViewModel(Tarea tarea)
        {
            tareaEditando = tarea;

            // Inicializar campos desde la tarea
            AgTitulo = tarea.Titulo;
            AgDescripcion = tarea.Descripcion;
            Estado = tarea.Estado == "Finalizada";

            ListaPrioridades = new List<string> { "Alta", "Media", "Baja" };
            PrioridadSeleccionada = tarea.Prioridad;

            // Etiquetas posibles
            var etiquetasPosibles = new List<string> { "Trabajo", "Estudios", "Personal", "Salud" };

            EtiquetasConSeleccion = new ObservableCollection<EtiquetaSeleccionada>(
                etiquetasPosibles.Select(nombre =>
                    new EtiquetaSeleccionada
                    {
                        Nombre = nombre,
                        Seleccionada = tarea.Etiquetas?.Any(et => et.Titulo == nombre) == true
                    })
            );

            LimpiarEtiquetasCommand = new Command(() =>
            {
                foreach (var etiqueta in EtiquetasConSeleccion)
                {
                    etiqueta.Seleccionada = false;
                }
            });

            AgTarea = new Command(GuardarTarea);
            VolverPaginaPrincipal = new Command(Volver);
        }

        // Método para manejar cambios en los CheckBox
        public void OnEtiquetaCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var etiqueta = checkBox.BindingContext as EtiquetaSeleccionada;

            if (etiqueta != null)
            {
                etiqueta.Seleccionada = e.Value;
            }
        }

        private async void GuardarTarea()
        {
            if (string.IsNullOrWhiteSpace(AgTitulo) ||
                string.IsNullOrWhiteSpace(AgDescripcion) ||
                PrioridadSeleccionada == null ||
                !EtiquetasConSeleccion.Any(e => e.Seleccionada))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor, completa todos los campos", "Aceptar");
                return;
            }

            if (tareaEditando != null)      //Si estamos EDITANDO una tarea que ya existe pasa esto
            {
                tareaEditando.Titulo = AgTitulo;
                tareaEditando.Descripcion = AgDescripcion;
                tareaEditando.Prioridad = PrioridadSeleccionada;
                tareaEditando.Estado = Estado ? "Finalizada" : "Pendiente";
                tareaEditando.Etiquetas = EtiquetasConSeleccion
                    .Where(e => e.Seleccionada)
                    .Select(e => new Etiqueta { Titulo = e.Nombre })
                    .ToList();

                App.TareaRepositorio.SaveItem(tareaEditando);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Tarea editada con éxito", "Aceptar");
            }

            else
            {       //Si estamos CREANDO una nueva tarea pasa esto
                var nuevaTarea = new Tarea
                {
                    Titulo = AgTitulo,
                    Descripcion = AgDescripcion,
                    Prioridad = PrioridadSeleccionada,
                    Estado = Estado ? "Finalizada" : "Pendiente",
                    Etiquetas = EtiquetasConSeleccion
                    .Where(e => e.Seleccionada)
                    .Select(e => new Etiqueta { Titulo = e.Nombre })
                    .ToList()
                };

                App.TareaRepositorio.SaveItem(nuevaTarea);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Tarea creada con éxito", "Aceptar");
            }

            LimpiarCampos();
        }

        private async void Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void LimpiarCampos()
        {
            AgTitulo = string.Empty;
            AgDescripcion = string.Empty;
            Estado = false;
            foreach (var etiqueta in EtiquetasConSeleccion)
            {
                etiqueta.Seleccionada = false;
            }
            PrioridadSeleccionada = ListaPrioridades[1];
        }
    }
}