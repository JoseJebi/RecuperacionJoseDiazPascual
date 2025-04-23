using PropertyChanged;
using RecuperacionJoseDiazPascual.MVVM.Models;
using RecuperacionJoseDiazPascual.MVVM.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AgregarViewModel
    {
        public string AgTitulo { get; set; }
        public string AgDescripcion { get; set; }
        public bool Estado { get; set; }

        public List<string> ListaPrioridades { get; set; }
        public string PrioridadSeleccionada { get; set; }

        // Cambiamos a ObservableCollection para que la UI se actualice automáticamente
        public List<Etiqueta> ListaEtiquetas { get; set; }
        public ObservableCollection<EtiquetaSeleccionada> EtiquetaSeleccionadas { get; set; }

        // Comandos
        private Command _agTarea;
        public ICommand AgTarea => _agTarea;
        public ICommand VolverPaginaPrincipal { get; }
        public ICommand GestionEtiquetas { get; }
        public ICommand LimpiarEtiquetasCommand { get; }

        private Tarea tareaEditando;

        public AgregarViewModel()
        {
            ListaPrioridades = new List<string> { "Alta", "Media", "Baja" };
            PrioridadSeleccionada = ListaPrioridades[1];

            ListaEtiquetas = App.EtiquetaRepositorio.GetItems();

            EtiquetaSeleccionadas = new ObservableCollection<EtiquetaSeleccionada>(
                ListaEtiquetas.Select(e => new EtiquetaSeleccionada
                {
                    Etiqueta = e,
                    Seleccionada = false
                })
            );

            foreach (var item in EtiquetaSeleccionadas)
            {
                item.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(EtiquetaSeleccionada.Seleccionada))
                    {
                        NotificarCambioGuardar();
                    }
                };
            }

            _agTarea = new Command(
                execute: ()=> 
                {
                    GuardarTarea();
                },
                canExecute: () =>
                {
                    return !string.IsNullOrWhiteSpace(AgTitulo) && !string.IsNullOrWhiteSpace(AgDescripcion) &&
                    EtiquetaSeleccionadas.Any(e => e.Seleccionada);
                }
            );


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

            ListaEtiquetas = App.EtiquetaRepositorio.GetItems();

            EtiquetaSeleccionadas = new ObservableCollection<EtiquetaSeleccionada>(
                ListaEtiquetas.Select(e => new EtiquetaSeleccionada
                {
                    Etiqueta = e,
                    Seleccionada = tarea.Etiquetas?.Any(et => et.Titulo == e.Titulo) ?? false
                })
            );

            foreach (var item in EtiquetaSeleccionadas)
            {
                item.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(EtiquetaSeleccionada.Seleccionada))
                    {
                        NotificarCambioGuardar();
                    }
                };
            }

            _agTarea = new Command(
                execute: () =>
                {
                    GuardarTarea();
                },
                canExecute: () =>
                {
                    return !string.IsNullOrWhiteSpace(AgTitulo) && !string.IsNullOrWhiteSpace(AgDescripcion) &&
                    EtiquetaSeleccionadas.Any(e => e.Seleccionada);
                }
            );

            VolverPaginaPrincipal = new Command(Volver);
            GestionEtiquetas = new Command(GestEtiquetas);
        }

        private async void GuardarTarea()
        {
            if (tareaEditando != null)
            {
                tareaEditando.Titulo = AgTitulo;
                tareaEditando.Descripcion = AgDescripcion;
                tareaEditando.Prioridad = PrioridadSeleccionada;
                tareaEditando.Estado = Estado ? "Finalizada" : "Pendiente";
                tareaEditando.Etiquetas = new ObservableCollection<Etiqueta>();

                foreach (var item in EtiquetaSeleccionadas.Where(e => e.Seleccionada))
                {
                    Etiqueta etiq = App.EtiquetaRepositorio.GetItem(e => e.Titulo == item.Etiqueta.Titulo);
                    tareaEditando.Etiquetas.Add(etiq);
                };

                tareaEditando.EtiquetasString();

                App.TareaRepositorio.SaveItemCascada(tareaEditando);
                await Application.Current.MainPage.DisplayAlert(
                    "Éxito", 
                    "Tarea editada con éxito", 
                    "Aceptar"
                );
            }
            else
            {
                var nuevaTarea = new Tarea
                {
                    Titulo = AgTitulo,
                    Descripcion = AgDescripcion,
                    Prioridad = PrioridadSeleccionada,
                    Estado = Estado ? "Finalizada" : "Pendiente",
                    Etiquetas = new ObservableCollection<Etiqueta>()
                };

                foreach ( var item in EtiquetaSeleccionadas.Where( x => x.Seleccionada)){
                    Etiqueta etiq = App.EtiquetaRepositorio.GetItem(e => e.Titulo == item.Etiqueta.Titulo);
                    nuevaTarea.Etiquetas.Add(etiq);
                };

                nuevaTarea.EtiquetasString();

                App.TareaRepositorio.SaveItemCascada(nuevaTarea);

                await Application.Current.MainPage.DisplayAlert(
                    "Éxito", 
                    "Tarea creada con éxito", 
                    "Aceptar"
                );
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

        public void NotificarCambioGuardar()
        {
            _agTarea.ChangeCanExecute();
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