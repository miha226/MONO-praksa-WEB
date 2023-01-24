using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarDealership.Service.Common;
using CarDealership.Model;
using CarDealership.Repository.Common;

namespace CarDealership.Service
{
    public class AdressService : IAdressService
    {
        private IAdressRepository adressRepository;

        public AdressService(IAdressRepository adressRepository)
        {
            this.adressRepository = adressRepository;
        }
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
