using AutoMapper;
using BusinessLoginLayer.DTOs.License;
using DataAccessLayer;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class LicenseProfile:Profile
    {
        public LicenseProfile()
        {
            CreateMap<License, LicenseDTO>()
                .ReverseMap();
        }
    }

}
