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
        private SQLiteConnection connection;
        private string _dbPath;
        private int result;

        public string StatusMessage;

        public CarService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (connection != null)
            {
                return;
            }

            connection = new SQLiteConnection(_dbPath);
            connection.CreateTable<Car>();
        }

        public List<Car> GetCars()
        {

            try
            {
                Init();
                return connection.Table<Car>().ToList();
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }

            return new List<Car>();
        }

        public Car GetCar(int id)
        {
            try
            {
                Init();
                return connection.Table<Car>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception exception)
            {
                StatusMessage = exception.Message;
            }

            return null;
        }

        public void AddCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                {
                    throw new Exception("Invalid Car Record");
                }

                result = connection.Insert(car);
                StatusMessage = result == 0 ? "Insert Failed" : "Insert Succeded";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to Insert data.";
            }
        }

        public int DeleteCar(int id)
        {
            try
            {
                Init();
                return connection.Table<Car>().Delete(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to delete data";
            }

            return 0;
        }
    }
}
