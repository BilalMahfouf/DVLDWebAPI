using BusinessLoginLayer.DTOs.Person;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.User
{
    public class ReadUserDTO
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; } = null!;
        public bool IsActive { get; set; }
        public ReadPersonDTO? Person { get; set; }
        public ReadUserDTO(int userID,int personID,string userName,bool isActive,
            ReadPersonDTO? person) 
        {
            UserID = userID;
            PersonID = personID;
            UserName = userName;
            IsActive = isActive;
            Person = person;
        }
        public ReadUserDTO(){ }
        }
}
