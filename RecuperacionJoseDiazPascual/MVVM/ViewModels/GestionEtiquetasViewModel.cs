using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        EditarEtiquetaCommand = new Command<Etiqueta>(EditarEtiqueta);
        EliminarEtiquetaCommand = new Command<Etiqueta>(EliminarEtiqueta);
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

    public async void EditarEtiqueta(Etiqueta etiqueta)
    {
        if (etiqueta != null)
        {
            string nuevoNombre = await Application.Current.MainPage.DisplayPromptAsync(
                "Editar Etiqueta",
                $"Modifica el nombre de la etiqueta \"{etiqueta.Titulo}\"",
                placeholder: "Nuevo nombre",
                initialValue: etiqueta.Titulo,
                maxLength: 50,
                keyboard: Keyboard.Text
            );

            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                bool yaExiste = Etiqueta.Any(e => e.Titulo == nuevoNombre);
                if (yaExiste && nuevoNombre != etiqueta.Titulo)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Aviso",
                        "Ya existe una etiqueta con ese nombre.",
                        "Aceptar"
                    );
                }
                else
                {
                    etiqueta.Titulo = nuevoNombre;
                    App.EtiquetaRepositorio.SaveItem(etiqueta);

                    var index = Etiqueta.IndexOf(etiqueta);
                    Etiqueta.RemoveAt(index);
                    Etiqueta.Insert(index, etiqueta);
                }
            }
        }
    }



    public async void EliminarEtiqueta(Etiqueta etiqueta)
    {
        if (etiqueta != null)
        {
            bool decision = await Application.Current.MainPage.DisplayAlert(
                                "Confirmación",
                                $"¿Estás seguro de que quieres eliminar la etiqueta \"{etiqueta.Titulo}\"?",
                                "Sí", "No"
                            );

            if (decision)
            {
                App.EtiquetaRepositorio.DeleteItem(etiqueta);
                Etiqueta.Remove(etiqueta);
            }

        }
        else
        {
            await Application.Current.MainPage.DisplayAlert(
                "Aviso",
                "Debes seleccionar una etiqueta",
                "Aceptar"
            );
        }
    }

    public async void Volver()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}
