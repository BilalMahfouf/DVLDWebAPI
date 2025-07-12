using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class DriverProfile:Profile
    {
        public DriverProfile()
        {
            CreateMap<DataAccessLayer.Driver, Core.DTOs.Driver.DriverDTO>()
                .ReverseMap();
        }
    }
}
