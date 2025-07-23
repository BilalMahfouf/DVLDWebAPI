using AutoMapper;
using Core.Common;
using Core.DTOs.Person;
using Core.Interfaces;
using Core.Interfaces.Repositories.People;
using Core.Interfaces.Services.People;
using Core.Shared;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public PersonService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<Result<int>> CreatePersonAsync(PersonDTO personDTO)
        {
            if(personDTO is null)
            {
               return Result<int>.Failure("Person DTO cannot be null.",Enums.ErrorType.BadRequest);
            }
            var person = _mapper.Map<Person>(personDTO);
            _uow.personRepository.Add(person);
            return await _uow.SaveChangesAsync() 
                ? Result<int>.Success(person.PersonID) 
                : Result<int>.Failure("Failed to create person.", Enums.ErrorType.InternalServerError);
        }

        public async Task<Result<bool>> DeletePersonAsync(int id)
        {
            if(id <= 0)
            {
                return Result<bool>.Failure("ID must be greater than zero.", Enums.ErrorType.BadRequest);
            }
           _uow.personRepository.Delete(id);
            return await _uow.SaveChangesAsync() 
                ? Result<bool>.Success(true) 
                : Result<bool>.Failure("Failed to delete person.", Enums.ErrorType.InternalServerError);
        }

        public async Task<Result<ReadPersonDTO?>> FindAsync(int id)
        {
           if(id<= 0)
            {
               return Result<ReadPersonDTO?>.Failure("ID must be greater than zero.", Enums.ErrorType.BadRequest);
            }
            var person = await _repo.FindAsync(p => p.PersonID == id);
            if(person is null)
            {
                return Result<ReadPersonDTO?>.Failure("Person not found", Enums.ErrorType.NotFound);
            }
            var personDto=_mapper.Map<ReadPersonDTO>(person);
            return Result<ReadPersonDTO?>.Success(personDto);
        }

        public async Task<Result<ReadPersonDTO?>> FindAsync(string nationalNo)
        {
            if(string.IsNullOrWhiteSpace(nationalNo))
            {
                throw new ArgumentNullException(nameof(nationalNo), "National number cannot be null or empty.");
            }
            var person = await _repo.FindAsync(p => p.NationalNo == nationalNo);
            if (person is null)
            {
                return Result<ReadPersonDTO?>.Failure("Person not found", Enums.ErrorType.NotFound);
            }
            var personDto = _mapper.Map<ReadPersonDTO>(person);
            return Result<ReadPersonDTO?>.Success(personDto);
        }

        public async Task<Result<IEnumerable<ReadPersonDTO>>> GetAllAsync()
        {
            var people = await _uow.personRepository.GetAllAsync();
            if (people == null || !people.Any())
            {
                return Result<IEnumerable<ReadPersonDTO>>.Failure("No persons found.", Enums.ErrorType.NotFound);
            }
            var peopleDto = _mapper.Map<IEnumerable<ReadPersonDTO>>(people);
            return Result<IEnumerable<ReadPersonDTO>>.Success(peopleDto);
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
            var person = await _repo.FindAsync(personID);
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person), "Person not found.");
            }
            _mapper.Map(personDTO, person);
            return await _repo.UpdateAsync(person);
        }
    }
}
