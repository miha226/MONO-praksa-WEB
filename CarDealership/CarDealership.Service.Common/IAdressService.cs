using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Model;

namespace CarDealership.Service.Common
{
    public interface IAdressService
    {
        List<Adress> Get();
        Adress Get(Guid id);
        ResponseWrapper<bool> Delete(Guid id);
        ResponseWrapper<bool> Post(Adress adress);
        ResponseWrapper<bool> ChangeAdress(Guid id, Adress adress);
    }
}
