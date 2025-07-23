using Core.DTOs.Person;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.People
{
    public interface IPersonService
    {
        Task<Result<ReadPersonDTO?>> FindAsync(int  id);
        Task<Result<ReadPersonDTO?>> FindAsync(string nationalNo);
        Task<Result<int>> CreatePersonAsync(PersonDTO personDTO);
        Task<Result<bool>> UpdatePersonAsync(int personID,PersonDTO personDTO);
        Task<Result<bool>> DeletePersonAsync(int id);
        Task<Result<bool>> IsExistAsync(int id);
        Task<Result<bool>> IsExistAsync(string nationalNo);
        Task<Result<IEnumerable<ReadPersonDTO>>> GetAllAsync();


    }
}
