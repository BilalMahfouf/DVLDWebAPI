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
    public class TestAppointmentProfile:Profile
    {
      public TestAppointmentProfile()
        {
            CreateMap<TestAppointment, TestAppointmentDTO>()
                .ReverseMap();
        }
    }
}
