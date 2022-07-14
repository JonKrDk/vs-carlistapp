using CarListApp.Maui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Services
{
    public class CarService
    {
        private SQLiteConnection connection = null;
        private string _dbPath;
        private string StatusMessage;

        public CarService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (connection is null)
            {
                connection = new SQLiteConnection(_dbPath);
                connection.CreateTable<Car>();
            }
        }

        public List<Car> GetCars()
        {

            try
            {
                Init();
                return connection.Table<Car>().ToList();
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrive data.";
            }

            return new List<Car>();

            //return new List<Car>()
            //{
            //    new Car { Id = 1, Make = "Honda",   Model = "Fit",    Vin = "123" },
            //    new Car { Id = 2, Make = "Toyota",  Model = "Prado",  Vin = "123" },
            //    new Car { Id = 3, Make = "Honda",   Model = "Civic",  Vin = "123" },
            //    new Car { Id = 4, Make = "Audi",    Model = "A5",     Vin = "123" },
            //    new Car { Id = 5, Make = "BMW",     Model = "M3",     Vin = "123" },
            //    new Car { Id = 6, Make = "Nissan",  Model = "Note",   Vin = "123" },
            //    new Car { Id = 7, Make = "Ferrari", Model = "Spider", Vin = "123" }
            //};
        }
    }
}
