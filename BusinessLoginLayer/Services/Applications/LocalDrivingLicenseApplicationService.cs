using AutoMapper;
using BusinessLoginLayer.Helpers;
using Core.Common;
using Core.DTOs.Application;
using Core.Interfaces;
using Core.Interfaces.Repositories.Applications;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Applications;
using Core.Shared;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Applications
{
    public class LocalDrivingLicenseApplicationService : ILocalDrivingLicenseApplicationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public LocalDrivingLicenseApplicationService(IMapper mapper
            ,
            IUnitOfWork uow)

        {
            _mapper = mapper;
            _uow = uow;
        }


        public async Task<GenericResult<int>> CreateLDLApplicationAsync
            (LocalDrivingLicenseDTO LDLapplication)
        {
            try
            {
                var validationResult = await LDLapplication
                .ValidateForCreateLocalDrivingLicenseApplicationAsync(_uow);
                if (!validationResult.IsSuccess)
                {
                    return GenericResult<int>.Failure(validationResult.ErrorMessage,
                        validationResult.ErrorType);
                }
                var localDLApp = _mapper.Map<LocalDrivingLicenseApplication>(LDLapplication);
                _uow.localDrivingLicenseApplicationRepository.Add(localDLApp);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return GenericResult<int>.Success
                        (localDLApp.LocalDrivingLicenseApplicationID);
                }
                return GenericResult<int>.Failure("LocalDrivingLicenseApplication can't be created",
                         Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return GenericResult<int>.Failure($"An error occurred while saving data to the " +
                    $"DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<Result> DeleteLDLApplicationAsync(int LDLapplicationID)
        {
            if( LDLapplicationID <= 0)
            {
                return Result.Failure("Invalid id", Enums.ErrorType.BadRequest);
            }
            try
            {
                var LDLApplication = await _uow.localDrivingLicenseApplicationRepository
                    .FindAsync
                (x => x.LocalDrivingLicenseApplicationID == LDLapplicationID);
                if (LDLApplication is null)
                {
                    return Result.Failure("LocalDrivingLicenseApplication not found.",
                        Enums.ErrorType.NotFound);
                }
                _uow.localDrivingLicenseApplicationRepository.Delete(LDLapplicationID);
                _uow.applicationRepository.Delete(LDLApplication.ApplicationID);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
                return Result.Failure("LocalDrivingLicenseApplication can't be deleted"
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while saving data to the " +
                    $"DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public  async Task<GenericResult<LocalDrivingLicenseDTO>>
            FindLDLAppByIDAsync(int LDLapplicationID)
        {
            if( LDLapplicationID <= 0)
            {
                return GenericResult<LocalDrivingLicenseDTO>.Failure("Invalid id",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var localDLApp = await _uow.localDrivingLicenseApplicationRepository
                    .FindAsync(x => x.LocalDrivingLicenseApplicationID
                    == LDLapplicationID);
                if(localDLApp is null)
                {
                    return GenericResult<LocalDrivingLicenseDTO>.Failure
                        ("LDLApplication not found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<LocalDrivingLicenseDTO>.Success
                    (_mapper.Map<LocalDrivingLicenseDTO>(localDLApp));
            }

            catch (Exception ex)
            {
                return GenericResult<LocalDrivingLicenseDTO>
                    .Failure($"An error occurred while retrieving data from the DB:" +
                    $" {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>>> GetAllAsync()
        {
            try
            {
                var DashboardData = await _uow.localDrivingLicenseApplicationRepository
                    .GetAll_ViewAsync();
                if(DashboardData is null || ! DashboardData.Any())
                {
                    return GenericResult
                        <IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>>
                        .Failure("data not found", Enums.ErrorType.NotFound);
                }
                return GenericResult<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>>
                    .Success(_mapper.Map<IEnumerable
                    <LocalDrivingLicenseApplicationDashboardDTO>>(DashboardData));
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable
                    <LocalDrivingLicenseApplicationDashboardDTO>>
                    .Failure($"An error occurred while retrieving data from the DB:" +
                    $" {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        
    }
}
