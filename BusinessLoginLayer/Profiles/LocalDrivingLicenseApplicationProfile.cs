using AutoMapper;
using Core.DTOs.Application;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class LocalDrivingLicenseApplicationProfile:Profile
    {
        public LocalDrivingLicenseApplicationProfile() {

            CreateMap<LocalDrivingLicenseApplication, LocalDrivingLicenseDTO>()
                .ReverseMap();
        }
    }
}
