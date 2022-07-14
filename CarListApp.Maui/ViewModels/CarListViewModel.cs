﻿using CarListApp.Maui.Models;
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
        // private readonly CarService carService;
        
        public ObservableCollection<Car> Cars { get; private set; } = new ObservableCollection<Car>();

        public CarListViewModel()
        {
            Title = "Car List";
            // this.carService = carService;
        }

        [ObservableProperty]
        bool isRefreshing;

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

                var cars = App.CarService.GetCars();
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
        async Task GetCarDetails(Car car)
        {
            if (car == null)
            {
                return;
            }

            await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
            {
                { nameof(Car), car }
            });
        }
    }
}
