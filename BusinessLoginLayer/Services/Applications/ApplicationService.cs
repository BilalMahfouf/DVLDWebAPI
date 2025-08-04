using AutoMapper;
using Core.Common;
using Core.DTOs.Application;
using Core.Interfaces;
using Core.Interfaces.Repositories.Applications;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Applications;
using Core.Shared;
using DataAccessLayer;
using DataAccessLayer.Repositories.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLoginLayer.Services.Applications
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;

        public ApplicationService( IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task <Result> CompleteApplicationAsync(int applicationID)
        {
            if (applicationID <= 0)
            {
                Result.Failure("ID must be greater then 0", Enums.ErrorType.BadRequest);
            }
            try
            {
                var application = await _uow.applicationRepository.
                               FindAsync(a => a.ApplicationID == applicationID);
                if (application is null)
                {
                    return Result.Failure("Application not found.",
                        Enums.ErrorType.NotFound);
                }
                application.ApplicationStatus = (byte)Enums.ApplicationStatusEnum.Completed;
                application.LastStatusDate = DateTime.UtcNow;
                _uow.applicationRepository.Update(application);
                return Result.Success;
            }
            catch (Exception ex)
            {
                return Result.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }
        public async Task<Result> CancelApplication(int applicationID)
        {

            if (applicationID <= 0)
            {
                Result.Failure("ID must be greater then 0", Enums.ErrorType.BadRequest);
            }
            try
            {
                var application = await _uow.applicationRepository.
                               FindAsync(a => a.ApplicationID == applicationID);
                if (application is null)
                {
                    return Result.Failure("Application not found.", Enums.ErrorType.NotFound);
                }
                application.ApplicationStatus = (byte)Enums.ApplicationStatusEnum.Canceled;
                application.LastStatusDate = DateTime.UtcNow;
                _uow.applicationRepository.Update(application);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
                return Result.Failure("This application can't be cancelled"
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }
        public async Task<GenericResult<int>> CreateApplicationAsync(ApplicationDTO application,
            Enums.ApplicationTypeEnum applicationType
            = Enums.ApplicationTypeEnum.NewLocalDrivingLicense)
        {
            if (application is null)
            {
                return GenericResult<int>.Failure("application dto is null"
                    , Enums.ErrorType.BadRequest);
            }
            try
            {
                var newApplication = _mapper.Map<Application>(application);
                newApplication.ApplicationTypeID = (byte)applicationType;
                newApplication.ApplicationStatus = (byte)Enums.ApplicationStatusEnum.New;
                newApplication.ApplicationDate = DateTime.UtcNow;
                newApplication.LastStatusDate = DateTime.UtcNow;
                _uow.applicationRepository.Add(newApplication);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return GenericResult<int>.Success(newApplication.ApplicationID);
                }
                return GenericResult<int>.Failure("This application can't be created"
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return GenericResult<int>.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

        public async Task<Result> DeleteApplicationAsync(int applicationID)
        {
            if (applicationID <= 0)
            {
                return Result.Failure("ID must be greater then 0",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                _uow.applicationRepository.Delete(applicationID);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
                return Result.Failure($"Application with id {applicationID} can't be deleted"
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure("an error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<ReadApplicationDTO?>> FindByIDAsync
            (int applicationID)
        {
            if (applicationID <= 0)
            {
                return GenericResult<ReadApplicationDTO?>.Failure("ID must be greater then 0",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var application = await _uow.applicationRepository.FindAsync
                                (a => a.ApplicationID == applicationID);
                if (application is null)
                {
                    return GenericResult<ReadApplicationDTO?>.Failure("Application not found."
                        , Enums.ErrorType.NotFound);
                }
                return GenericResult<ReadApplicationDTO?>.Success(_mapper.
                    Map<ReadApplicationDTO>(application));
            }
            catch (Exception ex)
            {
                return GenericResult<ReadApplicationDTO?>.Failure
                    ("an error occurred while retrieving data " +
                    $"from the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }

    }
}
