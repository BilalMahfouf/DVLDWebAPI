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
        Task<GenericResult<ReadUserDTO>> FindByIDAsync(int id);
        Task<GenericResult<IEnumerable<ReadUserDTO>>> GetAllAsync();
        Task<GenericResult<int>> CreateUserAsync(CreateUserDTO userDTO);
        Task<Result> UpdateUserAsync(int userID,UpdateUserDTO userDTO);
        Task<Result> DeleteUserAsync(int id);
        Task<Result> ActivateAsync(int id);
        Task<Result> DeActivateAsync(int id);
        Task<Result> CanCreateUserAsync(int personID);


    }
}
