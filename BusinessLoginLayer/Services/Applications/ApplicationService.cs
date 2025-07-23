using AutoMapper;
using Core.Common;
using Core.DTOs.Application;
using Core.Interfaces.Repositories.Applications;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Applications;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLoginLayer.Services.Applications
{
    public class ApplicationService : IApplicationService
    {
        protected readonly IApplicationRepository _applicationRepository;
        protected readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        private async Task <bool> _UpdateApplicationStatus(int applicationID, Enums.ApplicationStatusEnum status)
        {
            if (applicationID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(applicationID), "Application ID must be greater than zero.");
            }
            var application = await _applicationRepository.FindAsync(applicationID);
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application), "Application not found.");
            }
            application.ApplicationStatus = (byte)status;
            application.LastStatusDate = DateTime.UtcNow;
            return await _applicationRepository.UpdateAsync(application);
        }
        public async Task<bool> CancelApplication(int applicationID)
        {
            return await _UpdateApplicationStatus
                (applicationID, Enums.ApplicationStatusEnum.Canceled);
        }

        public async Task<bool> CompleteApplicationAsync(int applicationID)
        {
            return await _UpdateApplicationStatus
                (applicationID, Enums.ApplicationStatusEnum.Completed);
        }

        public async Task<int> CreateApplicationAsync(ApplicationDTO application,
            Enums.ApplicationTypeEnum applicationType
            = Enums.ApplicationTypeEnum.NewLocalDrivingLicense, Enums.ApplicationStatusEnum
            applicationStatus = Enums.ApplicationStatusEnum.New)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application), "Application cannot be null.");
            }
            var newApplication = _mapper.Map<Application>(application);
            newApplication.ApplicationTypeID = (byte)applicationType;
            newApplication.ApplicationDate=DateTime.UtcNow;
            newApplication.LastStatusDate = DateTime.UtcNow;
            newApplication.ApplicationStatus = (byte)applicationStatus;
            return await _applicationRepository.AddAsync(newApplication);
        }

        public async Task<bool> DeleteApplicationAsync(int applicationID)
        {
            if (applicationID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(applicationID), "Application ID must be greater than zero.");
            }
            return await _applicationRepository.DeleteAsync(applicationID);
        }

        public async Task<ReadApplicationDTO?> FindByIDAsync(int applicationID)
        {
            if (applicationID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(applicationID), "Application ID must be greater than zero.");
            }
            var application = await _applicationRepository.FindAsync(applicationID);
            return application is null ? null : _mapper.Map<ReadApplicationDTO>(application);
        }

        public async Task<bool> IsExistAsync(int applicationID)
        {
            if (applicationID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(applicationID), "Application ID must be greater than zero.");
            }
            return await _applicationRepository.IsExistAsync(applicationID);
        }
    }
}
