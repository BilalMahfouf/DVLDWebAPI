using AutoMapper;
using BusinessLoginLayer.Helpers;
using Core.Common;
using Core.DTOs.License;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Licenses;
using Core.Shared;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Licenses
{
    public class InternationalLicenseService : IInternationalLicenseService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILicenseService _licenseService;
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public InternationalLicenseService(IMapper mapper, ILicenseService licenseService
            , IApplicationService applicationService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _licenseService = licenseService;
            _applicationService = applicationService;
            _uow = uow;
        }

        private async Task<Result> _UpdateStatus(int id, bool isActive)
        {
         try
            {
                if (id <= 0)
                {
                    return Result.Failure("Invalid License ID", Enums.ErrorType.BadRequest);
                }
                var internationalLicense = await _uow.internationalLicenseRepository.
                     FindAsync(l => l.InternationalLicenseID == id);
                if (internationalLicense is null)
                {
                    return Result.Failure("International license not found.", Enums.ErrorType.NotFound);
                }
                internationalLicense.IsActive = isActive;
                _uow.internationalLicenseRepository.Update(internationalLicense);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to update international license status."
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while updating license status: {ex.Message}"
                    , Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<Result> ActivateAsync(int id)
        {
            return await _UpdateStatus(id, true);
        }

        public async Task<Result> DeActivateAsync(int id)
        {
            return await _UpdateStatus(id,false);
        }

        public async Task<Result> DeleteInternationalLicenseAsync(int id)
        {
           if(id <= 0)
            {
                return Result.Failure("ID must be greater than zero."
                    ,Enums.ErrorType.BadRequest);
            }
           try
            {
                if (!(await _uow.internationalLicenseRepository.IsExistAsync
                    (i => i.InternationalLicenseID == id))) 
                {
                    return Result.Failure("International license does not exist."
                        , Enums.ErrorType.BadRequest);
                }
                _uow.internationalLicenseRepository.Delete(id);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to delete international license."
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while updating saving data to DB: {ex.Message}"
                    , Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<ReadInternationalLicenseDTO?>>
            FindByIDAsync(int id)
        {
           if(id <=0)
            {
                return GenericResult<ReadInternationalLicenseDTO?>
                    .Failure("License ID must be greater than zero."
                    , Enums.ErrorType.BadRequest);
            }
           try
            {
                var license = await _uow.internationalLicenseRepository
                    .FindAsync(l => l.InternationalLicenseID == id);
                if(license is null)
                {
                    return GenericResult<ReadInternationalLicenseDTO?>
                        .Failure("International license not found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<ReadInternationalLicenseDTO?>
                    .Success(_mapper.Map<ReadInternationalLicenseDTO>(license));
            }
            catch (Exception ex)
            {
                return GenericResult<ReadInternationalLicenseDTO?>
                    .Failure($"An error occurred while retrieving data from the DB: " +
                    $"{ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<IEnumerable<ReadInternationalLicenseDTO>>>
            GetAllAsync()
        {
           try
            {
                var licenses =await _uow.internationalLicenseRepository.GetAllAsync();
                if(licenses is null || !licenses.Any())
                {
                    return GenericResult<IEnumerable<ReadInternationalLicenseDTO>>
                        .Failure("No international licenses found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<IEnumerable<ReadInternationalLicenseDTO>>.Success
                    (_mapper.Map<IEnumerable<ReadInternationalLicenseDTO>>(licenses));
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<ReadInternationalLicenseDTO>>
                    .Failure($"An error occurred while retrieving data from the DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<int>> IssueInternationalLicense
            (InternationalLicenseDTO licenseDTO)
        {
            var validationResult = await licenseDTO
                 .ValidateForCreateInternationalLicenseAsync(_uow);
            if (!validationResult.IsSuccess)
            {
                return GenericResult<int>.Failure
                    (validationResult.ErrorMessage, validationResult.ErrorType);
            }
                try
            {
                var license = _mapper.Map<InternationalLicense>(licenseDTO);
                license.IsActive = true;
                license.IssueDate = DateTime.UtcNow;
                // Assuming 5 years validity
                license.ExpirationDate = license.IssueDate.AddYears(5);
                var completeAppResult = await _applicationService.CompleteApplicationAsync
                        (licenseDTO.ApplicationID);
                if (!completeAppResult.IsSuccess)
                {
                    return GenericResult<int>.Failure(completeAppResult.ErrorMessage,
                        completeAppResult.ErrorType);
                }
                _uow.internationalLicenseRepository.Add(license);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return GenericResult<int>.Success(license.InternationalLicenseID);
                }
                return GenericResult<int>.Failure("Failed to issue international license.",
                    Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return GenericResult<int>
                    .Failure($"An error occurred while saving data to the DB: " +
                    $"{ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

    }
}
