using System.Collections.ObjectModel;
using System.Windows.Input;

public class GestionEtiquetasViewModel
{
    private readonly List<string> _listaOriginal;
    private readonly Action<List<string>> _onEtiquetasActualizadas;

    public ObservableCollection<string> ListaEtiquetas { get; set; }
    public string NombreEtiqueta { get; set; }
    public string EtiquetaSeleccionada { get; set; }

    public ICommand VolverAgregarTarea { get; }
    public ICommand GuardarEtiquetaCommand { get; }
    public ICommand EditarEtiquetaCommand { get; }
    public ICommand EliminarEtiquetaCommand { get; }

    public GestionEtiquetasViewModel()
    {

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
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}
