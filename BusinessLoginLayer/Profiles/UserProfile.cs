using AutoMapper;
using Core.DTOs.User;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User,ReadUserDTO>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>()
                .ForMember(u => u.PersonID, opt => opt.Ignore())
                .ForMember(u => u.UserID, opt => opt.Ignore());



                }
    }
}
