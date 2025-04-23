using RecuperacionJoseDiazPascual.MVVM.Models;
using RecuperacionJoseDiazPascual.MVVM.ViewModels;

namespace RecuperacionJoseDiazPascual.MVVM.Views;

public partial class AgregarView : ContentPage
{
	public AgregarView()
	{
		InitializeComponent();

		BindingContext = new AgregarViewModel();
	}

    /* Constructor secundario para abrir una tarea ya creada y poder editarla */
    public AgregarView(Tarea tarea)
    {
        InitializeComponent();

        BindingContext = new AgregarViewModel(tarea);
    }
}