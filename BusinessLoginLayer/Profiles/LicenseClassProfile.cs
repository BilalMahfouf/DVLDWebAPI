using AutoMapper;
using BusinessLoginLayer.Services;
using Core.DTOs.License;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class LicenseClassProfile:Profile
    {
        public LicenseClassProfile()
        {
            CreateMap<LicenseClass, LicenseClassDTO>().ReverseMap()
                .ForMember(dest => dest.Licenses, opt => opt.Ignore())
                .ForMember(dest => dest.LocalDrivingLicenseApplications, opt => opt.Ignore());
        }
    }
}
