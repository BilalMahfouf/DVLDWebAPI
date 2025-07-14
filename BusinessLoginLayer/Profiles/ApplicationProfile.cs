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
    public class ApplicationProfile:Profile
    {
        public ApplicationProfile() {

            CreateMap<ReadApplicationDTO, Application>();
            CreateMap<ApplicationDTO, Application>()
                .ForMember(a => a.ApplicationDate, opt => opt.Ignore())
                .ForMember(a => a.LastStatusDate, opt => opt.Ignore())
                .ForMember(a => a.ApplicationStatus, opt => opt.Ignore())
                .ForMember(a => a.ApplicationTypeID, opt => opt.Ignore());



        }
    }
}
