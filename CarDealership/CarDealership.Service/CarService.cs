using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarDealership.Service.Common;
using CarDealership.Model;
using CarDealership.Repository.Common;
using CarDealership.Common;

namespace CarDealership.Service
{
    public class CarService : ICarService
    {
        private ICarRepository carRepository;

        public CarService(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task<ResponseWrapper<bool>> Delete(Guid id)
        {
            return await carRepository.Delete(id);
        }

        public async Task<List<Car>> GetAsync(Sorting sorting, Paging paging, FilterCar filter)
        {
            paging.PageNumber -= 1;
            return await carRepository.GetAsync(sorting, paging, filter);
        }

        public async Task<Car> Get(Guid id)
        {
            return await carRepository.Get(id);
        }

        public async Task<ResponseWrapper<bool>> Post(Car car)
        {
            car.Id = Guid.NewGuid();
            return await carRepository.Post(car);
        }

        public async Task<ResponseWrapper<bool>> Put(Guid id, Car car)
        {
            return await carRepository.Put(id, car);
        }
    }
}
