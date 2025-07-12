using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.User
{
    public class CreateUserDTO
    {
        
        public int PersonID { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }

        public CreateUserDTO(int personID, string userName,
            string password, bool isActive)
        {
            PersonID = personID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }
        public CreateUserDTO() { }
    }
}
