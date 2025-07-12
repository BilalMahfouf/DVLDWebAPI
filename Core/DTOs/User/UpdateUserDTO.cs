using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.User
{
    public class UpdateUserDTO
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }
        public UpdateUserDTO(string userName, string password, bool isActive)
        {
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }
        public UpdateUserDTO(){ }
        }
}
