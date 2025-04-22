using System.Collections.ObjectModel;
using System.Windows.Input;
using RecuperacionJoseDiazPascual;
using RecuperacionJoseDiazPascual.MVVM.Models;

public class GestionEtiquetasViewModel
{
    public ObservableCollection<Etiqueta> Etiqueta{ get; set; }
    public string NombreEtiqueta { get; set; }

    public ICommand VolverAgregarTarea { get; }
    public ICommand GuardarEtiquetaCommand { get; }
    public ICommand EditarEtiquetaCommand { get; }
    public ICommand EliminarEtiquetaCommand { get; }

    public GestionEtiquetasViewModel()
    {
        // Recojo las etiquetas existentes en la base de datos
        Etiqueta = new ObservableCollection<Etiqueta>(App.EtiquetaRepositorio.GetItems());

        VolverAgregarTarea = new Command(Volver);
        GuardarEtiquetaCommand = new Command(GuardarEtiqueta);
        EditarEtiquetaCommand = new Command<string>(EditarEtiqueta);
        EliminarEtiquetaCommand = new Command<string>(EliminarEtiqueta);
    }

    public void GuardarEtiqueta()
    {
        if (!string.IsNullOrWhiteSpace(NombreEtiqueta))
        {
            bool existe = Etiqueta.Any(e => e.Titulo == NombreEtiqueta);

            if (existe)
            {
                Application.Current.MainPage.DisplayAlert(
                    "Aviso",
                    "La etiqueta ya existe.",
                    "Aceptar"
                );
            }
            else
            {
                var etiqueta = new Etiqueta { Titulo = NombreEtiqueta };
                App.EtiquetaRepositorio.SaveItem(etiqueta);
                Etiqueta.Add(etiqueta);
                NombreEtiqueta = string.Empty;
            }
        }
        else
        {
            Application.Current.MainPage.DisplayAlert(
                "Aviso",
                "El nombre de la etiqueta no puede estar vacío.",
                "Aceptar"
            );
        }

    }

    public void EditarEtiqueta(string etiqueta)
    {
        
    }

    public void EliminarEtiqueta(string etiqueta)
    {
        
    }

    public async void Volver()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}
