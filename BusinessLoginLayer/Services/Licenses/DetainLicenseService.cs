using AutoMapper;
using BusinessLoginLayer.Helpers;
using BusinessLoginLayer.Services.Applications;
using Core.Common;
using Core.DTOs.Application;
using Core.DTOs.Detain;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Licenses;
using Core.Shared;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Licenses
{
    public class DetainLicenseService : IDetainLicenseService
    {

        private readonly IUnitOfWork _uow;
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public DetainLicenseService( IApplicationService applicationService
            , IMapper mapper, IUnitOfWork uow)
        {
            _applicationService = applicationService;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<GenericResult<int>> CreateDetainedLicenseAsync
            (DetainLicenseDTO detainedLicenseDTO)
        {
            var validationResult = await detainedLicenseDTO
                .ValidateForCreateDetainedLicenseAsync(_uow);
            if(!validationResult.IsSuccess)
            {
                return GenericResult<int>.Failure(validationResult.ErrorMessage,
                    validationResult.ErrorType);
            }

            try
            {
                var detainedLicense = _mapper.Map<DetainedLicense>(detainedLicenseDTO);
                detainedLicense.IsReleased = false;
                detainedLicense.DetainDate = DateTime.UtcNow;
                _uow.detainedLicenseRepository.Add(detainedLicense);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return GenericResult<int>
                        .Success(detainedLicense.DetainID);
                }
                return GenericResult<int>.Failure("Failed to create detained license",
                    Enums.ErrorType.Conflict);
            }
            catch(Exception ex)
            {
                return GenericResult<int>.Failure("An error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<Result> DeleteDetainedLicenseAsync(int id)
        {
           if(id<=0)
            {
                return Result.Failure("Invalid ID", Enums.ErrorType.BadRequest);
            }
           try
           {
              if(!(await _uow.detainedLicenseRepository.IsExistAsync(d=>d.DetainID==id)))
              {
               return Result.Failure("Detained license not found"
                   , Enums.ErrorType.NotFound);
              }
                _uow.detainedLicenseRepository.Delete(id);
                var result = await _uow.SaveChangesAsync(); 
                if(result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to delete detained license"
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<DetainLicenseDTO?>> FindAsync(int id)
        {
            if (id <= 0)
            {
                return GenericResult<DetainLicenseDTO?>.Failure("ID must be greater than zero.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var detainedLicense = await _uow.detainedLicenseRepository.FindAsync
                    (d => d.DetainID == id);
                if(detainedLicense is null)
                {
                    return GenericResult<DetainLicenseDTO?>.Failure("Detained license not found.",
                        Enums.ErrorType.NotFound);
                }
                return GenericResult<DetainLicenseDTO?>
                    .Success(_mapper.Map<DetainLicenseDTO>(detainedLicense));
            }
            catch (Exception ex)
            {
                return GenericResult<DetainLicenseDTO?>.Failure
                    ("an error occurred while retrieving data " +
                    $"from the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<GenericResult<IEnumerable<DetainLicenseDTO>>> GetAllAsync()
        {
            try
            {
                var dLicenses = await _uow.detainedLicenseRepository.GetAllAsync();
                if (dLicenses is null || !dLicenses.Any())
                {
                    return GenericResult<IEnumerable<DetainLicenseDTO>>
                          .Failure("No detained licenses found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<IEnumerable<DetainLicenseDTO>>
                    .Success(_mapper.Map<IEnumerable<DetainLicenseDTO>>(dLicenses));
            }
            catch(Exception ex)
            {
                return GenericResult<IEnumerable<DetainLicenseDTO>>
                    .Failure($"An error occurred while retrieving data from the DB: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }
           
        }

        public async Task<Result> ReleaseLicenseAsync
            (UpdateDetainedLicenseDTO releaseDTO)
        {
            var validationResult = await releaseDTO
                .ValidateForUpdateDetainedLicenseAsync(_uow);
            if(!validationResult.IsSuccess)
            {
                return Result.Failure(validationResult.ErrorMessage,
                    validationResult.ErrorType);
            }
            try
            {
                var detainedLicense = await _uow.detainedLicenseRepository.FindAsync
                (d => d.DetainID == releaseDTO.DetainID);
                if (detainedLicense is null)
                {
                    return Result.Failure("Detained license not found.",
                        Enums.ErrorType.NotFound);
                }
                _mapper.Map(releaseDTO, detainedLicense);
                detainedLicense.IsReleased = true;
                detainedLicense.ReleaseDate = DateTime.UtcNow;
                var completeAppResult = await _applicationService
                     .CompleteApplicationAsync(releaseDTO.ReleaseApplicationID);
                if (!completeAppResult.IsSuccess)
                {
                    return Result.Failure(completeAppResult.ErrorMessage,
                        completeAppResult.ErrorType);
                }
                _uow.detainedLicenseRepository.Update(detainedLicense);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to release detained license",
                    Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure("An error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }
    }
}
