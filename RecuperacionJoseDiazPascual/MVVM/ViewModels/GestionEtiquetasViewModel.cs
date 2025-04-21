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
        public ObservableCollection<string> ListaEtiquetas { get; set; }
        private ObservableCollection<string> _listaOriginal;
        public string NombreEtiqueta { get; set; }
        public string EtiquetaSeleccionada { get; set; }

        public ICommand VolverAgregarTarea { get; }
        public ICommand GuardarEtiquetaCommand { get; }
        public ICommand EditarEtiquetaCommand { get; }
        public ICommand EliminarEtiquetaCommand { get; }

        public GestionEtiquetasViewModel()
        {
            ListaEtiquetas = new ObservableCollection<string> { "Trabajo", "Estudios", "Personal", "Salud", "Compras", "Viajes", "Ocio", "Mantenimiento" };
            _listaOriginal = new ObservableCollection<string>(ListaEtiquetas);

            VolverAgregarTarea = new Command(Volver);
            GuardarEtiquetaCommand = new Command(GuardarEtiqueta);
            EditarEtiquetaCommand = new Command<string>(EditarEtiqueta);
            EliminarEtiquetaCommand = new Command<string>(EliminarEtiqueta);
        }

        public void GuardarEtiqueta()
        {
            if (!string.IsNullOrWhiteSpace(NombreEtiqueta))
            {
                if (!string.IsNullOrEmpty(EtiquetaSeleccionada))
                {
                    int index = ListaEtiquetas.IndexOf(EtiquetaSeleccionada);
                    if (index != -1)
                    {
                        ListaEtiquetas[index] = NombreEtiqueta;
                    }
                }
                else if (!ListaEtiquetas.Contains(NombreEtiqueta))
                {
                    ListaEtiquetas.Add(NombreEtiqueta);
                }

                NombreEtiqueta = string.Empty;
                EtiquetaSeleccionada = string.Empty;
            }
        }

        public void EditarEtiqueta(string etiqueta)
        {
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
            if (!ListaEtiquetas.SequenceEqual(_listaOriginal))
            {
                bool respuesta = await Application.Current.MainPage.DisplayAlert(
                    "Cambios detectados",
                    "¿Deseas conservar los cambios en las etiquetas?",
                    "Sí", "No");
            }

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}