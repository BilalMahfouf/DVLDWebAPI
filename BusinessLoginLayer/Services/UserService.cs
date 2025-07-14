using AutoMapper;
using Core.DTOs.User;
using Core.Interfaces.Repositories.Users;
using Core.Interfaces.Services.Users;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> ActivateAsync(int id)
        {
         if(id<= 0)
         {
             throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
         }
         var user = await _userRepository.FindByIDAsync(id);
            if(user is null)
            {
                throw new ArgumentNullException(nameof(user), "User not found.");
            }
            user.IsActive = true;
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> CanCreateUserAsync(int personID)
        {
            if(personID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(personID), "Person ID must be greater than zero.");
            }
            return !(await _userRepository.isExistByPersonID(personID));
        }

        public Task<int> CreateUserAsync(CreateUserDTO userDTO)
        {
            if(userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "User DTO cannot be null.");
            }
            var user = _mapper.Map<User>(userDTO);
            var insertedID = _userRepository.AddAsync(user);
            return insertedID;
        }

        public async Task<bool> DeActivateAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var user = await _userRepository.FindByIDAsync(id);
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user), "User not found.");
            }
            user.IsActive = false;
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<ReadUserDTO?> FindByIDAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var user = await _userRepository.FindByIDAsync(id);
            return user is null ? null : _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<IEnumerable<ReadUserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if(users is null || !users.Any())
            {
                return Enumerable.Empty<ReadUserDTO>();
            }
            return _mapper.Map<IEnumerable<ReadUserDTO>>(users);
        }

        public async Task<bool> UpdateUserAsync(int userID,UpdateUserDTO userDTO)
        {
            if(userID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userID), "User ID must be greater than zero.");
            }
            if (userDTO is null)
            {
                throw new ArgumentNullException(nameof(userDTO), "User DTO cannot be null.");
            }
            var user = await _userRepository.FindByIDAsync(userID);
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user), "User not found.");
            }
            _mapper.Map(userDTO, user);
            return await _userRepository.UpdateAsync(user);   
        }
    }
}
