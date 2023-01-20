using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model;

namespace CarDealership.Repository.Common
{
    public interface ICarRepository
    {
        List<Car> Get();
        Car Get(Guid id);
        ResponseWrapper<bool> Post(Car car);
        ResponseWrapper<bool> Put(Guid id, Car car);
        ResponseWrapper<bool> Delete(Guid id);
    }
}
