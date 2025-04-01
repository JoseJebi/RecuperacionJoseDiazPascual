using RecuperacionJoseDiazPascual.MVVM.ViewModels;

namespace RecuperacionJoseDiazPascual.MVVM.Views;

public partial class PrincipalView : ContentPage
{
	public PrincipalView()
	{
		InitializeComponent();
		BindingContext = new PrinicipalViewModel();
	}
}