using AutoMapper;
using Core.Common;
using Core.DTOs.Application;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Shared;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Applications
{
    public class ApplicationTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ApplicationTypeService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<GenericResult<IEnumerable<ReadApplicationDTO>>> GetAllApplicationTypesAsync()
        {
            try
            {
                var applicationTypes = await _uow.applicationTypeRepository.GetAllAsync();
                if (applicationTypes is null || !applicationTypes.Any())
                {
                    return GenericResult<IEnumerable<ReadApplicationDTO>>.Failure("No application types found.", Enums.ErrorType.NotFound);
                }
                var readApplicationTypesDTO = _mapper.Map<IEnumerable<ReadApplicationDTO>>(applicationTypes);
               return GenericResult<IEnumerable<ReadApplicationDTO>>.Success(readApplicationTypesDTO);
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<ReadApplicationDTO>>
                    .Failure($"An error occurred while retrieving data from the DB:" +
                    $" {ex.Message}", Enums.ErrorType.InternalServerError);
            }


        }
    
        public async Task<Result> UpdateFeesAsync(int applicationTypeID,decimal fees)
        {
            if (applicationTypeID <= 0 || fees < 0) 
            {
                return Result.Failure("Application type ID must be greater " +
                    "than zero. and fees must be greater or equal 0"
                    , Enums.ErrorType.BadRequest);
            }
            try
            {
                var applicationType = await _uow.applicationTypeRepository.
                               FindAsync(a => a.ApplicationTypeID == applicationTypeID);
                if (applicationType is null)
                {
                    return Result.Failure("Application type not found.", Enums.ErrorType.NotFound);
                }
                applicationType.ApplicationFees = fees;
                _uow.applicationTypeRepository.Update(applicationType);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
            }
            catch(Exception ex)
            {
                return Result.Failure($"An error occurred while saving data to the " +
                    $"DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }
            return Result.Failure("Failed to update application type fees."
                , Enums.ErrorType.Conflict);
        }

        public async Task<GenericResult<ReadApplicationDTO?>> FindByIDAsync(int id)
        {
            if (id <= 0)
            {
               return GenericResult<ReadApplicationDTO?>.Failure("Invalid ID", Enums.ErrorType.BadRequest);
            }
            try
            {
                var applicationType = await _uow.applicationTypeRepository
               .FindAsync(a => a.ApplicationTypeID == id);
                if (applicationType is null)
                {
                    return GenericResult<ReadApplicationDTO?>.Failure("Application type not found.", Enums.ErrorType.NotFound);
                }
                var applicationTypeDto = _mapper.Map<ReadApplicationDTO>(applicationType);
                return GenericResult<ReadApplicationDTO?>.Success(applicationTypeDto);
            }
            catch(Exception ex)
            {
                return GenericResult<ReadApplicationDTO?>.Failure($"An error occurred while" +
                    $" retrieving data from the DB: {ex.Message}"
                    , Enums.ErrorType.InternalServerError);
            }

        }
    }
}
