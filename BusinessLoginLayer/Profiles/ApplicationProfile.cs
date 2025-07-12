using AutoMapper;
using BusinessLoginLayer.DTOs.Application;
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

            CreateMap<ReadApplicationDTO, Application>().ReverseMap();
        
        }
    }
}
