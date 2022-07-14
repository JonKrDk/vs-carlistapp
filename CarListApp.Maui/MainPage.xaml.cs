using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(CarListViewModel carListViewModel) 
	{
		InitializeComponent();
		BindingContext = carListViewModel;
	}
}

