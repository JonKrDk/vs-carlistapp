using CarListApp.Maui.Services;

namespace CarListApp.Maui;

public partial class App : Application
{
	public static CarService CarService { get; private set; }

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
		CarService = CarService;
	}
}
