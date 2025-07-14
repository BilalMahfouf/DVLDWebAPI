using AutoMapper;
using Core.DTOs.Driver;
using DataAccessLayer;
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
            CreateMap<Driver, ReadDriverDTO>();
            CreateMap<ReadDriverDTO, Driver>()
                .ForMember(d => d.CreatedDate, opt => opt.Ignore());
            CreateMap<Drivers_View, DriverDashboardDTO>();
        }
    }
}
