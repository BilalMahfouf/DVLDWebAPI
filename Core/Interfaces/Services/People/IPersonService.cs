using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.People
{
    public interface IPersonService<T> where T : class
    {
        Task<T?> FindAsync(int  id);
        Task<T?> FindAsync(string nationalNo);
        Task<int> CreatePersonAsync(T personDTO);
        Task<bool> UpdatePersonAsync(T personDTO);
        Task<bool> DeletePersonAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistAsync(string nationalNo);


    }
}
