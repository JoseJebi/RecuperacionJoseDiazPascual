using PropertyChanged;
using RecuperacionJoseDiazPascual.MVVM.Models;
using RecuperacionJoseDiazPascual.MVVM.Views;
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
        public string? AgTitulo { get; set; }
        public string? AgDescripcion { get; set; }
        public bool Estado { get; set; }

        public List<string> ListaPrioridades { get; set; }
        public string PrioridadSeleccionada { get; set; }

        // Cambiamos a ObservableCollection para que la UI se actualice automáticamente
        public ObservableCollection<string> ListaEtiquetas { get; set; }


        // Comandos
        public ICommand AgTarea { get; }
        public ICommand VolverPaginaPrincipal { get; }
        public ICommand GestionEtiquetas { get; }
        public ICommand LimpiarEtiquetasCommand { get; }
        private Tarea tareaEditando;

        public AgregarViewModel()
        {
            ListaPrioridades = new List<string> { "Alta", "Media", "Baja" };
            PrioridadSeleccionada = ListaPrioridades[1];

            AgTarea = new Command(GuardarTarea);
            VolverPaginaPrincipal = new Command(Volver);
            GestionEtiquetas = new Command(GestEtiquetas);
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


            AgTarea = new Command(GuardarTarea);
            VolverPaginaPrincipal = new Command(Volver);
            GestionEtiquetas = new Command(GestEtiquetas);
        }

        private async void GuardarTarea()
        {
            if (string.IsNullOrWhiteSpace(AgTitulo) ||
                string.IsNullOrWhiteSpace(AgDescripcion) ||
                PrioridadSeleccionada == null)
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
                    Estado = Estado ? "Finalizada" : "Pendiente"
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

        private async void GestEtiquetas()
        {

            await Application.Current.MainPage.Navigation.PushAsync(
                new GestionEtiquetasView()
            );
        }


        private void LimpiarCampos()
        {
            AgTitulo = string.Empty;
            AgDescripcion = string.Empty;
            Estado = false;
            PrioridadSeleccionada = ListaPrioridades[1];
        }
    }
}