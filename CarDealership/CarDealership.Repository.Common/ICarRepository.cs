﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model;

namespace CarDealership.Repository.Common
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAsync();
        Task<Car> Get(Guid id);
        Task<ResponseWrapper<bool>> Post(Car car);
        Task<ResponseWrapper<bool>> Put(Guid id, Car car);
        Task<ResponseWrapper<bool>> Delete(Guid id);
    }
}
