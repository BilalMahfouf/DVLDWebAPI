using AutoMapper;
using BusinessLoginLayer.DTOs.Person;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class PersonProfile:Profile
    {
        PersonProfile() 
        {
            CreateMap<Person, ReadPersonDTO>();
            CreateMap<PersonDTO, Person>();
        
        }
    }
}
