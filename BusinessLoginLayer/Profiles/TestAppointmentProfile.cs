using AutoMapper;
using Core.Common;
using Core.DTOs.Test;
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
                .ForMember(dest => dest.TestType
                , opt => opt.MapFrom(src => src.TestTypeID.ToString()));
            CreateMap<TestAppointments_View, TestAppointmentDTO>();
        }
    }
}
