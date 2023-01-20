using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model;

namespace CarDealership.Service.Common
{
    public interface ICarService
    {
        List<Car> Get();
        Car Get(Guid id);
        ResponseWrapper<bool> Post(Car car);
        ResponseWrapper<bool> Put(Guid id, Car car);
        ResponseWrapper<bool> Delete(Guid id);
    }
}
