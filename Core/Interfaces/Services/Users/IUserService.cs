using Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<ReadUserDTO?> FindByIDAsync(int id);
        Task<IEnumerable<ReadUserDTO>> GetAllAsync();
        Task<int> CreateUserAsync(CreateUserDTO userDTO);
        Task<bool> UpdateUserAsync(int userID,UpdateUserDTO userDTO);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ActivateAsync(int id);
        Task<bool> DeActivateAsync(int id);
        Task<bool> CanCreateUserAsync(int personID);


    }
}
