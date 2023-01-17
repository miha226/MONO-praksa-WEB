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
        public bool UpdateCarsKilometersTraveled(int id, int newKilometers)
        {
            Car selectedCar = cars.Find(x => x.Id == id);
            if (selectedCar!= null)
            {
                selectedCar.KilometersTravelled=newKilometers;
                return true;
            }
            return false;
            
        }
        public bool UpdateCarsKilometersTraveled(Car car, int newKilometers)
        {
            Car selectedCar = cars.Find(x => x.Id == car.Id && x.KilometersTravelled==car.KilometersTravelled);
            if (selectedCar != null)
            {
                selectedCar.KilometersTravelled = newKilometers;
                return true;
            }
            return false;
        }
        public bool RemoveCarFromList(int id)
        {
            return cars.Remove(cars.Find(car => car.Id == id));
        }
        public bool RemoveCarFromList(Car car)
        {
           
            return cars.Remove(cars.Find(x => x.Id == car.Id));
        }
    }
}