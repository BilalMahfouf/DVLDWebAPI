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
            try
            {
                var person = _mapper.Map<Person>(personDTO);
                _uow.personRepository.Add(person);
                return await _uow.SaveChangesAsync()
                    ? Result<int>.Success(person.PersonID)
                    : Result<int>.Failure("Failed to create person.", Enums.ErrorType.Conflict);
            }
            catch(Exception ex)
            {
                return Result<int>.Failure($"An error occurred while creating the person: {ex.Message}", Enums.ErrorType.InternalServerError);
            }
            
        }

        public async Task<Result<bool>> DeletePersonAsync(int id)
        {
            if(id <= 0)
            {
                return Result<bool>.Failure("ID must be greater than zero.", Enums.ErrorType.BadRequest);
            }
            try
            {
                _uow.personRepository.Delete(id);
                return await _uow.SaveChangesAsync()
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Failed to delete person.", Enums.ErrorType.Conflict);
            }
            catch(Exception ex)
            {
                return Result<bool>.Failure($"An error occurred while deleting the person: {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<Result<ReadPersonDTO?>> FindAsync(int id)
        {
           if(id<= 0)
            {
               return Result<ReadPersonDTO?>.Failure("ID must be greater than zero.", Enums.ErrorType.BadRequest);
            }
           try
            {
                var person = await _uow.personRepository.
                    FindAsync(p => p.PersonID == id);
                if (person is null)
                {
                    return Result<ReadPersonDTO?>.Failure("Person not found", Enums.ErrorType.NotFound);
                }
                var personDto = _mapper.Map<ReadPersonDTO>(person);
                return Result<ReadPersonDTO?>.Success(personDto);
            }
            catch(Exception ex)
            {
                return Result<ReadPersonDTO?>.Failure($"An error occurred while finding the person: {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<Result<ReadPersonDTO?>> FindAsync(string nationalNo)
        {
            if(string.IsNullOrWhiteSpace(nationalNo))
            {
                return Result<ReadPersonDTO?>.Failure("Invalid national no ", Enums.ErrorType.BadRequest);
            }
            try
            {
                var person = await _uow.personRepository.
                FindAsync(p => p.NationalNo == nationalNo);
                if (person is null)
                {
                    return Result<ReadPersonDTO?>.Failure("Person not found", Enums.ErrorType.NotFound);
                }
                var personDto = _mapper.Map<ReadPersonDTO>(person);
                return Result<ReadPersonDTO?>.Success(personDto);
            }
            catch (Exception ex)
            {
                return Result<ReadPersonDTO?>.Failure($"An error occurred while finding the person: {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<Result<IEnumerable<ReadPersonDTO>>> GetAllAsync()
        {
            try
            {
                var people = await _uow.personRepository.GetAllAsync();
                if (people == null || !people.Any())
                {
                    return Result<IEnumerable<ReadPersonDTO>>.Failure("No persons found.", Enums.ErrorType.NotFound);
                }
                var peopleDto = _mapper.Map<IEnumerable<ReadPersonDTO>>(people);
                return Result<IEnumerable<ReadPersonDTO>>.Success(peopleDto);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ReadPersonDTO>>.Failure($"An error occurred while finding the list of people: {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<Result<bool>> IsExistAsync(int id)
        {
           if(id <= 0)
            {
                return Result<bool>.Failure("ID must be greater than zero.", Enums.ErrorType.BadRequest);
            }

            var result= await _uow.personRepository.IsExistAsync(p => p.PersonID == id);
            return result 
                ? Result<bool>.Success(true) 
                : Result<bool>.Failure("Person does not exist.", Enums.ErrorType.NotFound);
        }

        public async Task<Result<bool>> IsExistAsync(string nationalNo)
        {
           if(string.IsNullOrWhiteSpace(nationalNo))
            {
                return Result<bool>.Failure("National number cannot be null or empty.", Enums.ErrorType.BadRequest);
            }
            var result = await _uow.personRepository
                .IsExistAsync(p => p.NationalNo == nationalNo);
            return result
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Person does not exist.", Enums.ErrorType.NotFound);
        }

        public async Task<Result<bool>> UpdatePersonAsync(int personID,PersonDTO personDTO)
        {
            if (personDTO is null || personID <= 0) 
            {
                return Result<bool>.Failure("Person DTO cannot be null or id must greater then 0"
                    , Enums.ErrorType.BadRequest);
            }
            try
            {
                var person = await _uow.personRepository.FindAsync(p => p.PersonID == personID);
                if (person is null)
                {
                    return Result<bool>.Failure("Person not found.", Enums.ErrorType.NotFound);
                }
                _mapper.Map(personDTO, person);
                _uow.personRepository.Update(person);
                return await _uow.SaveChangesAsync()
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Failed to update person.", Enums.ErrorType.Conflict);
            }
            catch(Exception ex)
            {
                return Result<bool>.Failure($"An error occurred while saving to the " +
                    $"data tp the  db {ex.Message}");
            }
           
        }
    }
}
