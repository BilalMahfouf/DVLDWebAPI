using Core.DTOs.User;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<Result<ReadUserDTO?>> FindByIDAsync(int id);
        Task<Result<IEnumerable<ReadUserDTO>>> GetAllAsync();
        Task<Result<int>> CreateUserAsync(CreateUserDTO userDTO);
        Task<Result<bool>> UpdateUserAsync(int userID,UpdateUserDTO userDTO);
        Task<Result<bool>> DeleteUserAsync(int id);
        Task<Result<bool>> ActivateAsync(int id);
        Task<Result<bool>> DeActivateAsync(int id);
        Task<Result<bool>> CanCreateUserAsync(int personID);


    }
}
