using AutoMapper;
using BusinessLoginLayer.Helpers;
using Core.Common;
using Core.DTOs.License;
using Core.DTOs.Test;
using Core.Interfaces;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Licenses;
using Core.Shared;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLoginLayer.Services.Licenses
{
    public class LicenseService : ILicenseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IApplicationService _applicationService;

        public LicenseService(IUnitOfWork uow, IMapper mapper, IApplicationService applicationService)
        {
            _uow = uow;
            _mapper = mapper;
            _applicationService = applicationService;
        }

        private async Task<Result> _UpdateLicenseStatus(int id,bool isActive)
        {
            if (id <= 0)
            {
                return Result.Failure("ID must be greater than zero."
                    ,Enums.ErrorType.BadRequest);
            }
            try
            {
                var license = await _uow.licenseRepository.FindAsync(l => l.LicenseID == id);
                if (license is null)
                {
                    return Result.Failure("License not found."
                        , Enums.ErrorType.NotFound);
                }
                license.IsActive = isActive;
                _uow.licenseRepository.Update(license);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;

                }
                return Result.Failure("Failed to update license status."
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while updating license status: {ex.Message}"
                    , Enums.ErrorType.InternalServerError);
            }

        }
        public async Task<Result> ActivateLicenseAsync(int id)
        {
            return await _UpdateLicenseStatus(id, true);
        }

        public async Task<Result> DeActivateLicenseAsync(int id)
        {
            return await _UpdateLicenseStatus(id, false);
        }

        public async Task<Result> DeleteLicenseAsync(int id)
        {
            if (id <= 0)
            {
                return Result.Failure("ID must be greater than zero."
                    , Enums.ErrorType.BadRequest);
            }
            try
            {
                //var license = await _uow.licenseRepository.FindAsync
                //    (l => l.LicenseID == id);
                //if (license is null)
                //{
                //    return Result.Failure("License not found."
                //        , Enums.ErrorType.NotFound);
                //}
                //if (license.IsActive)
                //{
                //    return Result.Failure("Cannot delete an active license."
                //        , Enums.ErrorType.Conflict);
                //}
                var isExist = await _uow.licenseRepository.IsExistAsync
                    (l => l.LicenseID == id);
                if (!isExist)
                {
                    return Result.Failure("License not found."
                        , Enums.ErrorType.NotFound);
                }
                _uow.licenseRepository.Delete(id);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to delete license."
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while deleting license: {ex.Message}"
                    , Enums.ErrorType.InternalServerError);
            }
        }
        

        public async Task<GenericResult<ReadLicenseDTO>> FindByIDAsync(int id)
        {
           if(id <= 0)
            {
                return GenericResult<ReadLicenseDTO>.
                    Failure("ID must be greater than zero.", Enums.ErrorType.BadRequest);
            }
            try
            {
                var license = await _uow.licenseRepository.FindAsync
                    (l => l.LicenseID == id);
                if(license is null)
                {
                    return GenericResult<ReadLicenseDTO>.
                        Failure("License not found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<ReadLicenseDTO>.
                    Success(_mapper.Map<ReadLicenseDTO>(license));
            }
            catch (Exception ex)
            {
                return GenericResult<ReadLicenseDTO>
                    .Failure($"An error occurred while retrieving data from the DB: " +
                    $"{ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<GenericResult<IEnumerable<ReadLicenseDTO>>> GetAllLicenseAsync()
        {
            try
            {
                var licenses = await _uow.licenseRepository.GetAllAsync(null!,"LicenseClass");
                if (licenses is null || !licenses.Any())
                {
                    return GenericResult<IEnumerable<ReadLicenseDTO>>
                        .Failure("No licenses found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<IEnumerable<ReadLicenseDTO>>.Success
                    (_mapper.Map<IEnumerable<ReadLicenseDTO>>(licenses));
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<ReadLicenseDTO>>
                    .Failure($"An error occurred while retrieving data from the DB: " +
                    $"{ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        private async Task<GenericResult<int>>  _CreateLicenseAsync(LicenseDTO licenseDTO,
            Enums.IssueReason issueReason=Enums.IssueReason.FirstTime)
        {
            var validationResult = await licenseDTO.ValidateForCreateLicenseAsync
                (_uow);
            if(!validationResult.IsSuccess)
            {
                return GenericResult<int>.Failure(validationResult.ErrorMessage
                    , validationResult.ErrorType);
            }
            try
            {
                var license = _mapper.Map<License>(licenseDTO);


                license.IssueDate = DateTime.UtcNow;
                var licenseValidityLength = await Constants.LicenseValidityLength
                    ((int)licenseDTO.LicenseClass, _uow);

                license.ExpirationDate = license.IssueDate.AddYears(licenseValidityLength);
                license.IsActive = true;
                license.IssueReason = (byte)issueReason;
               var completeApplicationResult= await _applicationService
                    .CompleteApplicationAsync(licenseDTO.ApplicationID);
                if(!completeApplicationResult.IsSuccess)
                {
                    GenericResult<int>.Failure(completeApplicationResult.ErrorMessage
                        , completeApplicationResult.ErrorType);
                }
                _uow.licenseRepository.Add(license);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return GenericResult<int>.Success(license.LicenseID);
                }
                return GenericResult<int>.Failure("License can't be created",
                    Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return GenericResult<int>
                    .Failure($"An error occurred while saving data to the DB: " +
                    $"{ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }
            
        public async Task<GenericResult<int>> IssueNewDrivingLicenseAsync(LicenseDTO licenseDTO)
        {
            return await _CreateLicenseAsync(licenseDTO, Enums.IssueReason.FirstTime);
        }
        public async Task<GenericResult<int>> RenewLicenseAsync(int oldLicenseID,LicenseDTO licenseDTO)
        {
            var validationResult = await _ValidateForRenewLicense(oldLicenseID);
            if(!validationResult.IsSuccess)
            {
                return GenericResult<int>.Failure(validationResult.ErrorMessage,
                    validationResult.ErrorType);
            }

            return await _CreateLicenseAsync(licenseDTO, Enums.IssueReason.Renew);

        }
        public async Task<GenericResult<int>> IssueReplacementForLostLicenseAsync(int oldLicenseID
            , LicenseDTO licenseDTO)
        {
            var validationResult = await _ValidateForIssueReplacement(oldLicenseID);
            if(!validationResult.IsSuccess)
            {
                return GenericResult<int>.Failure(validationResult.ErrorMessage,
                    validationResult.ErrorType);
            }
            return await _CreateLicenseAsync(licenseDTO, Enums.IssueReason.ReplacementForLost);
        }
        public async Task<GenericResult<int>> IssueReplacementForDamagedLicenseAsync(int oldLicenseID
            , LicenseDTO licenseDTO)
        {
            var validationResult = await _ValidateForIssueReplacement(oldLicenseID);
            if (!validationResult.IsSuccess)
            {
                return GenericResult<int>.Failure(validationResult.ErrorMessage,
                    validationResult.ErrorType);
            }
            return await _CreateLicenseAsync(licenseDTO, Enums.IssueReason.ReplacementForDamaged);
        }

        private async Task<Result> _ValidateForIssueReplacement(int oldLicenseID)
        {
            if (oldLicenseID <= 0)
            {
                return Result.Failure("Invalid license id", Enums.ErrorType.BadRequest);
            }
            var oldLicense = await _uow.licenseRepository.FindAsync
                (l => l.LicenseID == oldLicenseID);
            if(oldLicense is null)
            {
                return Result.Failure("License does not exist", Enums.ErrorType.BadRequest);
            }
            if(!oldLicense.IsActive)
            {
                return Result.Failure("License is not active", Enums.ErrorType.BadRequest);
            }
            if (DateTime.Compare(oldLicense.ExpirationDate, DateTime.Now) <= 0)
            {
                return Result.Failure("License is expired", Enums.ErrorType.BadRequest);
            }
            return Result.Success;
        }
        private async Task<Result> _ValidateForRenewLicense(int oldLicenseID)
        {
            if (oldLicenseID <= 0)
            {
                return Result.Failure("Invalid license id", Enums.ErrorType.BadRequest);
            }
            var oldLicense = await _uow.licenseRepository.FindAsync
                (l => l.LicenseID == oldLicenseID);
            if (oldLicense is null)
            {
                return Result.Failure("License does not exist", Enums.ErrorType.BadRequest);
            }
            if (!oldLicense.IsActive)
            {
                return Result.Failure("License is not active", Enums.ErrorType.BadRequest);
            }
            if (DateTime.Compare(oldLicense.ExpirationDate, DateTime.Now) > 0)
            {
                return Result.Failure("License is not expired", Enums.ErrorType.BadRequest);
            }
            return Result.Success;
        }

    }
}
