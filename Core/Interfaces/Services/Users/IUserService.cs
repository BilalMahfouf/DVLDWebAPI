using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Users
{
    public interface IUserService<T> where T : class
    {
        Task<T?> FindByIDAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CreateUserAsync(T userDTO);
        Task<bool> UpdateUserAsync(T userDTO);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ActivateAsync(int id);
        Task<bool> DeActivateAsync(int id);
        Task<bool> CanCreateUserAsync(int personID);


    }
}
