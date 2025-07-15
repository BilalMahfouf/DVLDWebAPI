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
    public class LocalDrivingLicenseApplicationService : ILocalDrivingLicenseApplicationService
    {
        private readonly ILocalDrivingLicenseApplicationRepository _localDLAppRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationService _applicationService;
        public LocalDrivingLicenseApplicationService(IMapper mapper
            , ILocalDrivingLicenseApplicationRepository localDLAppRepository
            ,IApplicationService applicationService)

        {
            _localDLAppRepository = localDLAppRepository;
            _applicationService = applicationService;
            _mapper = mapper;
        }
        public async Task<bool> CanCreateLDLApplication(int personID, Enums.LicenseClassTypeEnum licenseClassID)
        {
           if( personID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(personID), "Person ID must be greater than zero.");
            }
            return !await _localDLAppRepository.IsExistNewAppAsync(personID, (int)licenseClassID);
        }

        public async Task<int> CreateLDLApplicationAsync(LocalDrivingLicenseDTO LDLapplication)
        {
            if(LDLapplication is null)
            {
                throw new ArgumentNullException(nameof(LDLapplication)
                    , "Local Driving License application cannot be null.");
            }
            if (!await _applicationService.IsExistAsync(LDLapplication.ApplicationID))
            {
                throw new InvalidOperationException(nameof(LDLapplication.ApplicationID)
                    + " This Application Don't Exist.");
            }
            var localDLApp = _mapper.Map<LocalDrivingLicenseApplication>(LDLapplication);
            var insertedID = await _localDLAppRepository.AddAsync(localDLApp);
            return insertedID;
        }

        public async Task<bool> DeleteLDLApplicationAsync(int LDLapplicationID)
        {
            if( LDLapplicationID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(LDLapplicationID)
                , "Local Driving License application ID must be greater than zero.");
            }
            if(!await _localDLAppRepository.DeleteAsync(LDLapplicationID))
                return false;
            var localDLApp = await _localDLAppRepository.FindByIDAsync(LDLapplicationID);
            if(localDLApp is null)
            {
                throw new InvalidOperationException(nameof(LDLapplicationID)
                    + " This Local Driving License application ID doesn't exist.");
            }
            return await _applicationService.DeleteApplicationAsync(LDLapplicationID);
        }

        public  async Task<LocalDrivingLicenseDTO?> FindLDLAppByIDAsync(int LDLapplicationID)
        {
            if( LDLapplicationID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(LDLapplicationID)
                , "Local Driving License application ID must be greater than zero.");
            }
            var localDLApp = await _localDLAppRepository.FindByIDAsync(LDLapplicationID);
            return localDLApp is null ? null : _mapper
                .Map<LocalDrivingLicenseDTO>(localDLApp);
        }

        public async Task<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>> GetAllAsync()
        {
            var applications = await _localDLAppRepository.GetAll_ViewAsync();
            if (applications is null || !applications.Any())
            {
                Enumerable.Empty<LocalDrivingLicenseApplicationDashboardDTO>();
            }
            return _mapper.Map<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>>(applications);

        }

        
    }
}
