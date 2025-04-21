using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;


namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GestionEtiquetasViewModel
    {
        // Propiedades
        public ObservableCollection<string> ListaEtiquetas { get; set; }
        public string NombreEtiqueta { get; set; } 
        public string EtiquetaSeleccionada { get; set; } 

        // Comandos
        public ICommand VolverAgregarTarea { get; }
        public ICommand GuardarEtiquetaCommand { get; }
        public ICommand EditarEtiquetaCommand { get; }
        public ICommand EliminarEtiquetaCommand { get; }

        public GestionEtiquetasViewModel()
        {
            // Inicializa la lista con tus etiquetas predefinidas
            ListaEtiquetas = new ObservableCollection<string> { "Trabajo", "Estudios", "Personal", "Salud", "Compras", "Viajes", "Ocio", "Mantenimiento" };

            // Comandos
            VolverAgregarTarea = new Command(Volver);
            GuardarEtiquetaCommand = new Command(GuardarEtiqueta);
            EditarEtiquetaCommand = new Command<string>(EditarEtiqueta);
            EliminarEtiquetaCommand = new Command<string>(EliminarEtiqueta);
        }

        // Guarda la etiqueta creada
        public void GuardarEtiqueta()
        {
            if (!string.IsNullOrWhiteSpace(NombreEtiqueta))
            {
                // Modo edición (reemplaza la etiqueta seleccionada)
                if (!string.IsNullOrEmpty(EtiquetaSeleccionada))
                {
                    int index = ListaEtiquetas.IndexOf(EtiquetaSeleccionada);
                    if (index != -1)
                    {
                        ListaEtiquetas[index] = NombreEtiqueta;
                    }
                }
                // Modo añadir (solo si no existe)
                else if (!ListaEtiquetas.Contains(NombreEtiqueta))
                {
                    ListaEtiquetas.Add(NombreEtiqueta);
                }

                // Limpia el Entry y la selección
                NombreEtiqueta = string.Empty;
                EtiquetaSeleccionada = string.Empty;
            }
        }

        public void EditarEtiqueta(string etiqueta)
        {
            // Carga la etiqueta en el Entry para editar
            NombreEtiqueta = etiqueta;
            EtiquetaSeleccionada = etiqueta;
        }

        public void EliminarEtiqueta(string etiqueta)
        {
            if (ListaEtiquetas.Contains(etiqueta))
            {
                ListaEtiquetas.Remove(etiqueta);
            }
        }

        public async void Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}