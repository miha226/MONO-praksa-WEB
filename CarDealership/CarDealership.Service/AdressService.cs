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
    public class AdressService : IAdressService
    {
        private static AdressRepository adressRepository = new AdressRepository();

        public async Task<List<Adress>> Get()
        {
            return await adressRepository.Get();
        }

       
        public async Task<Adress> Get(Guid id)
        {
            return await adressRepository.Get(id);
        }

       
        public async Task<ResponseWrapper<bool>> Post(Adress adress)
        {
            adress.AdressId = Guid.NewGuid();
            return await adressRepository.Post(adress);
        }

        

        public async Task<ResponseWrapper<bool>> ChangeAdress(Guid id, Adress adress)
        {
            return await adressRepository.ChangeAdress(id, adress);
        }


        public async Task<ResponseWrapper<bool>> Delete(Guid id)
        {
            return await adressRepository.Delete(id);
        }
    }
}
