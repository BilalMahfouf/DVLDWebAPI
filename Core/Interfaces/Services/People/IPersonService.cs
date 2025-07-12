using Core.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.People
{
    public interface IPersonService
    {
        Task<ReadPersonDTO?> FindAsync(int  id);
        Task<PersonDTO?> FindAsync(string nationalNo);
        Task<int> CreatePersonAsync(PersonDTO personDTO);
        Task<bool> UpdatePersonAsync(int personID,PersonDTO personDTO);
        Task<bool> DeletePersonAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistAsync(string nationalNo);
        Task<IEnumerable<ReadPersonDTO>> GetAllAsync();


    }
}
