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

        public List<Adress> Get()
        {
            return adressRepository.Get();
        }

       
        public Adress Get(Guid id)
        {
            return adressRepository.Get(id);
        }

       
        public ResponseWrapper<bool> Post(Adress adress)
        {
            return adressRepository.Post(adress);
        }

        

        public ResponseWrapper<bool> ChangeAdress(Guid id, Adress adress)
        {
            return adressRepository.ChangeAdress(id, adress);
        }


        public ResponseWrapper<bool> Delete(Guid id)
        {
            return adressRepository.Delete(id);
        }
    }
}
