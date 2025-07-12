using AutoMapper;
using BusinessLoginLayer.DTOs.Test;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class TestTypeProfile:Profile
    {
        public TestTypeProfile()
        {
            CreateMap<TestType, TestTypeDTO>();
            CreateMap<TestTypeDTO, TestType>();
        }
    }
}
