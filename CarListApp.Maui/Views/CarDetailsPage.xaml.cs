using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class CarDetailsPage : ContentPage
{
	public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
	{
		InitializeComponent();

		BindingContext = carDetailsViewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		// Do some fanciness

		// Default action
		base.OnNavigatedTo(args);
	}
}