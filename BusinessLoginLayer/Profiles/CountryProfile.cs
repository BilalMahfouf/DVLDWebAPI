using AutoMapper;
using Core.DTOs.Country;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class CountryProfile:Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, ReadCountryDTO>();
        }
    }
}
