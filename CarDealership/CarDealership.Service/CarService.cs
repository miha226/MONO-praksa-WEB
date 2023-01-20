using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Service.Common;
using CarDealership.Repository;
using CarDealership.Model;

namespace CarDealership.Service
{
    public class CarService : ICarService
    {
        private static CarRepository carRepository = new CarRepository();

        public ResponseWrapper<bool> Delete(Guid id)
        {
            return carRepository.Delete(id);
        }

        public List<Car> Get()
        {
            return carRepository.Get();
        }

        public Car Get(Guid id)
        {
            return carRepository.Get(id);
        }

        public ResponseWrapper<bool> Post(Car car)
        {
            return carRepository.Post(car);
        }

        public ResponseWrapper<bool> Put(Guid id, Car car)
        {
            return carRepository.Put(id, car);
        }
    }
}
