using AutoMapper;
using Core.DTOs.License;
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
            CreateMap<LicenseDTO, License>()
                .ForMember(l => l.IssueDate, opt => opt.Ignore())
                .ForMember(l => l.ExpirationDate, opt => opt.Ignore())
                .ForMember(l => l.IsActive, opt => opt.Ignore());

            CreateMap<License, ReadLicenseDTO>();

        }
    }

}
