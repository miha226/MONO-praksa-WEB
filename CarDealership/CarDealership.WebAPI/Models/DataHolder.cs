using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.WebAPI.Models
{
    public class DataHolder
    {
        public static List<Car> cars;
        public DataHolder()
        {
            cars = new List<Car>();
        }

        public void AddCar(Car car) {
            cars.Add(car); }
        public List<Car> GetCars()
        {
            return cars;
        }
        public Car GetCar(int id) { return cars.Find(x => x.Id == id); }
        public void UpdateCarsKilometersTraveled(int id, int newKilometers)
        {
            cars.Find(x => x.Id == id).KilometersTravelled = newKilometers;
        }
        public void UpdateCarsKilometersTraveled(Car car, int newKilometers)
        {
            cars.Find(x => x == car).KilometersTravelled = newKilometers;
        }
        public bool RemoveCarFromList(int id)
        {
            return cars.Remove(cars.Find(car => car.Id == id));
        }
        public bool RemoveCarFromList(Car car)
        {
            return cars.Remove(car);
        }
    }
}