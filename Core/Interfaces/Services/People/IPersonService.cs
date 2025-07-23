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
        Task<GenericResult<ReadPersonDTO?>> FindAsync(int  id);
        Task<GenericResult<ReadPersonDTO?>> FindAsync(string nationalNo);
        Task<GenericResult<int>> CreatePersonAsync(PersonDTO personDTO);
        Task<Result> UpdatePersonAsync(int personID,PersonDTO personDTO);
        Task<Result> DeletePersonAsync(int id);
        Task<Result> IsExistAsync(int id);
        Task<Result> IsExistAsync(string nationalNo);
        Task<GenericResult<IEnumerable<ReadPersonDTO>>> GetAllAsync();


    }
}
