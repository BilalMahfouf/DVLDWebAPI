using AutoMapper;
using Core.DTOs.Test;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class TestProfile: Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestDTO>()
                .ReverseMap();
           
        }
    }
}
