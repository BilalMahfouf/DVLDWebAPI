using AutoMapper;
using Core.DTOs.Person;
using Core.Interfaces.Repositories.People;
using Core.Interfaces.Services.People;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services
{
    
    public class PersonService:IPersonService
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;
        public PersonService(IPersonRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> CreatePersonAsync(PersonDTO personDTO)
        {
            if(personDTO == null)
            {
                throw new ArgumentNullException(nameof(personDTO), "Person DTO cannot be null.");
            }
            var person = _mapper.Map<Person>(personDTO);
            var insertedID = await _repo.AddAsync(person);
            return insertedID;
        }

        public Task<bool> DeletePersonAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            return _repo.DeleteAsync(id);
        }

        public async Task<ReadPersonDTO?> FindAsync(int id)
        {
           if(id<= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var person = await _repo.FindByIDAsync(id);
            if(person is null)
            {
                return null;
            }
            return _mapper.Map<ReadPersonDTO>(person);
        }

        public async Task<PersonDTO?> FindAsync(string nationalNo)
        {
            if(string.IsNullOrWhiteSpace(nationalNo))
            {
                throw new ArgumentNullException(nameof(nationalNo), "National number cannot be null or empty.");
            }
            var person=await _repo.FindByNationalNoAsync(nationalNo);
            if(person is null)
            {
                return null;
            }
            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<IEnumerable<ReadPersonDTO>> GetAllAsync()
        {
            var people = await _repo.GetAllAsync();
            if (people == null || !people.Any())
            {
                return Enumerable.Empty<ReadPersonDTO>();
            }
            return _mapper.Map<IEnumerable<ReadPersonDTO>>(people);
        }

        public async Task<bool> IsExistAsync(int id)
        {
           if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            return await _repo.IsExistAsync(id);
        }

        public async Task<bool> IsExistAsync(string nationalNo)
        {
           if(string.IsNullOrWhiteSpace(nationalNo))
            {
                throw new ArgumentNullException(nameof(nationalNo), "National number cannot be null or empty.");
            }
            return await _repo.IsExistAsync(nationalNo);
        }

        public async Task<bool> UpdatePersonAsync(int personID,PersonDTO personDTO)
        {
            if(personDTO is null)
            {
                throw new ArgumentNullException(nameof(personDTO), "Person DTO cannot be null.");
            }
            var person = await _repo.FindByIDAsync(personID);
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person), "Person not found.");
            }
            _mapper.Map(personDTO, person);
            return await _repo.UpdateAsync(person);
        }
    }
}
