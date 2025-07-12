using AutoMapper;
using Core.DTOs.Application.ApplicationType;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class ApplicationTypeProfile:Profile
    {
        public ApplicationTypeProfile()
        {
            CreateMap<ApplicationType, ApplicationTypeDTO>();

        }
    }
}
