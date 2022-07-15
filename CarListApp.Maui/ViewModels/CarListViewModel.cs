using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        public ObservableCollection<Car> Cars { get; private set; } = new ObservableCollection<Car>();

        public CarListViewModel(CarApiService carApiService)
        {
            Title = "Car List";
            GetCarList().Wait();
            this.carApiService = carApiService;
        }

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string make;

        [ObservableProperty]
        string model;

        [ObservableProperty]
        string vin;
        private readonly CarApiService carApiService;

        [ICommand]
        async Task GetCarList()
        {
            if (IsLoading)
            {
                return;
            }
            try
            {
                IsLoading = true;

                if(Cars.Any())
                {
                    Cars.Clear();
                }

                // var cars = App.CarService.GetCars();
                var cars = await carApiService.GetCars();
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Unable to get cars : {exception.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrive list of cars", "Ok");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [ICommand]
        async Task GetCarDetails(int id)
        {
            if (id == 0)
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

        [ICommand]
        async Task AddCar()
        {
            if ( string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await Shell.Current.DisplayAlert("Inavlid Data", "Please enter valid data", "Ok");
                return;
            }

            var car = new Car
            {
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            App.CarService.AddCar(car);

            await Shell.Current.DisplayAlert("Information", App.CarService.StatusMessage, "Ok");
            await GetCarList();
        }

        [ICommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }

            var result = App.CarService.DeleteCar(id);

            if (result == 0)
            {
                await Shell.Current.DisplayAlert("Failed", "Please enter valid data", "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Deletion Successful", "Record removed succesfully", "Ok");
                await GetCarList();
            }
        }
    }
}
