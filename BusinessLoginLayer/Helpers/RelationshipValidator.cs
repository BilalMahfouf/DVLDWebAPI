using Core.Common;
using Core.DTOs.User;
using Core.Interfaces;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Helpers
{
    public static class RelationshipValidator
    {
        public static async Task<Result<bool>> ValidateForCreateUserAsync
            (this CreateUserDTO userDto, IUnitOfWork uow)
        {
            var result = await uow.userRepository.IsExistAsync(u => u.PersonID == userDto.PersonID);
            return result ? Result<bool>.Failure
                ("this user already exist", Enums.ErrorType.Conflict) : Result<bool>.Success(true);
        }
    }
}
