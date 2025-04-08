using RecuperacionJoseDiazPascual.MVVM.ViewModels;

namespace RecuperacionJoseDiazPascual.MVVM.Views;

public partial class AgregarView : ContentPage
{
	public AgregarView()
	{
		InitializeComponent();

		BindingContext = new AgregarViewModel();
	}
}