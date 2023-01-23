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
        Task<List<Adress>> Get();
        Task<Adress> Get(Guid id);
        Task<ResponseWrapper<bool>> Delete(Guid id);
        Task<ResponseWrapper<bool>> Post(Adress adress);
        Task<ResponseWrapper<bool>> ChangeAdress(Guid id, Adress adress);
    }
}
