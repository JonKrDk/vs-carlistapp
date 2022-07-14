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
    }
}
