using AutoMapper;
using Core.Common;
using Core.DTOs.User;
using Core.Interfaces;
using Core.Interfaces.Repositories.Users;
using Core.Interfaces.Services.Users;
using Core.Shared;
using DataAccessLayer;
using DataAccessLayer.Repositories.User;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper,IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        private async Task<Result<bool>> _UpdateUserIsActiveStatus(int id,bool isActive)
        {
            if(id <= 0)
         {
                return Result<bool>.Failure("ID must be greater than zero.");
            }
            try
            {
                var user = await _uow.userRepository.FindAsync(u=>u.UserID==id);
                if (user is null)
                {
                    return Result<bool>.Failure("User not found", Enums.ErrorType.NotFound);
                }
                user.IsActive = isActive;
                _uow.userRepository.Update(user);
                var result = await _uow.SaveChangesAsync();
                return result ? Result<bool>.Success(true) : Result<bool>.Failure(
                    "Can't updated user is active status", Enums.ErrorType.Conflict);
            }
            catch(Exception ex)
            {
                return Result<bool>.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}",Enums.ErrorType.InternalServerError);
            }
            

        }
        public async Task<Result<bool>> ActivateAsync(int id)
        {
            return await _UpdateUserIsActiveStatus(id, true);
        }

        public async Task<Result<bool>> CanCreateUserAsync(int personID)
        {
            if (personID <= 0)
            {
                Result<bool>.Failure("personID must be greater than zero.",
                     Enums.ErrorType.BadRequest);
            }
            try
            {
                var isExist = await _uow.userRepository.IsExistAsync(u => u.PersonID == personID);
                return isExist ?
                    Result<bool>.Failure("User already exists for this personID.",
                    Enums.ErrorType.Conflict) :
                    Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("an error occurred while retrieving data " +
                    $"from the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<Result<int>> CreateUserAsync(CreateUserDTO userDTO)
        {
            if(userDTO == null)
            {
                return Result<int>.Failure("user dto can't be null", Enums.ErrorType.BadRequest);
            }
            var createUserValidationResult=await CanCreateUserAsync(userDTO.PersonID);
            if(!createUserValidationResult.IsSuccess)
            {
                return Result<int>.Failure(createUserValidationResult.ErrorMessage,
                    createUserValidationResult.ErrorType);
            }
            try
            {
                var user = _mapper.Map<User>(userDTO);
                _uow.userRepository.Add(user);
                var result= await _uow.SaveChangesAsync();
                if(result)
                {

                }
                return result ? Result<int>.Success(user.UserID) : Result<int>.
                    Failure("can't add this user", Enums.ErrorType.Conflict);
            }
             catch(Exception ex)
            {
                return Result<int>.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<Result<bool>> DeActivateAsync(int id)
        {
            return await _UpdateUserIsActiveStatus(id, false);
        }

        public async Task<Result<bool>> DeleteUserAsync(int id)
        {
            if(id <= 0)
            {
                Result<bool>.Failure("invalid id", Enums.ErrorType.BadRequest);
            }
            try
            {
              _uow.userRepository.Delete(id);
                return await _uow.SaveChangesAsync() ? Result<bool>.Success(true)
                    : Result<bool>.Failure
                    ("can't delete this user", Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<Result<ReadUserDTO?>> FindByIDAsync(int id)
        {
            if(id <= 0)
            {
                return Result<ReadUserDTO?>.Failure("invalid user id", Enums.ErrorType.BadRequest);
            }
            try
            {
                var user = await _uow.userRepository.FindAsync(u => u.UserID == id);
                if(user is null)
                {
                    return Result<ReadUserDTO?>.Failure("User not found.", Enums.ErrorType.NotFound);
                }
                return Result<ReadUserDTO?>.Success(_mapper.Map<ReadUserDTO>(user));
            }
            catch (Exception ex)
            {
                return Result<ReadUserDTO?>.Failure("an error occurred while retrieving data " +
                    $"from the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }


        }

        public async Task<Result<IEnumerable<ReadUserDTO>>> GetAllAsync()
        {
            try
            {
                var users = await _uow.userRepository.GetAllAsync();
                if( users is null || !users.Any())
                {
                    return Result<IEnumerable<ReadUserDTO>>
                        .Failure("Users not found.", Enums.ErrorType.NotFound);
                }
                return Result<IEnumerable<ReadUserDTO>>.Success(_mapper.Map
                    <IEnumerable<ReadUserDTO>>(users));
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ReadUserDTO>>.Failure("an error occurred while retrieving data " +
                    $"from the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<Result<bool>> UpdateUserAsync(int userID,UpdateUserDTO userDTO)
        {
            if(userID <= 0 || userDTO is null)
            {
               return Result<bool>.Failure("Invalid user ID or userDTO."
                   , Enums.ErrorType.BadRequest);
            }
           try
            {
                var user = await _uow.userRepository.FindAsync(u => u.UserID == userID);
                if(user is null)
                {
                    return Result<bool>.Failure("User not found.", Enums.ErrorType.NotFound);
                }
                _mapper.Map(userDTO, user);
                _uow.userRepository.Update(user);
                var result = await _uow.SaveChangesAsync();
                return result ? Result<bool>.Success(true) : Result<bool>.
                    Failure("can't update this user", Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }
    }
}
