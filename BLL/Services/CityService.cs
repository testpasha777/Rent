using BLL.Interface;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CityService : ICityService
    {
        private ICityRepository cityRep;

        public CityService(ICityRepository _cityRep)
        {
            cityRep = _cityRep;
        }
    }
}
