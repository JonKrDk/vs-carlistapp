using CarListApp.Maui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Services
{
    public class CarApiService
    {
        HttpClient _httpClient;

        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8001" : "http://localhost:8001";
        public string StatusMessage;

        public CarApiService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public async Task<List<Car>> GetCars()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("/cars");
                return JsonConvert.DeserializeObject<List<Car>>(response);
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }

            return null;
        }

        public async Task<Car> GetCar(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync("/cars/" + id);
                return JsonConvert.DeserializeObject<Car>(response);
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }

            return null;
        }

        public async Task AddCar(Car car)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/cars/", car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Insert Successfull";
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        public async Task DeleteCar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("/cars/" + id);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Delete Successfull";
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }

        public async Task UpdateCar(int id, Car car)
        {
            try
            {
                 var response = await _httpClient.PutAsJsonAsync("/cars/" + id, car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Update Successfull";
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }
        }
    }
}
