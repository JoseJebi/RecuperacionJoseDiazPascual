using System.Collections.ObjectModel;
using RecuperacionJoseDiazPascual.MVVM.ViewModels;

namespace RecuperacionJoseDiazPascual.MVVM.Views;

public partial class GestionEtiquetasView : ContentPage
{
	public GestionEtiquetasView(ObservableCollection<string> etiquetas, Action<List<string>> onEtiquetasActualizadas)
	{
		InitializeComponent();

		BindingContext = new GestionEtiquetasViewModel(etiquetas, onEtiquetasActualizadas);
	}
}