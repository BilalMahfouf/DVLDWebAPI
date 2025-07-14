using AutoMapper;
using Core.Common;
using Core.DTOs.License;
using Core.Interfaces.Repositories.Licenses;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Licenses;
using DataAccessLayer;
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
        private readonly ILicenseRepository _licenseRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationService _applicationService;
        private readonly LicenseClassService _licenseClassService;
        public LicenseService(ILicenseRepository licenseRepository, IMapper mapper
            , IApplicationService applicationService, LicenseClassService licenseClassService)
        {
            _licenseRepository = licenseRepository;
            _mapper = mapper;
            _applicationService = applicationService;
            _licenseClassService = licenseClassService;
        }


        private async Task<bool> _UpdateLicenseStatus(int id,bool isActive)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var license = await _licenseRepository.FindByIDAsync(id);
            if (license is null)
            {
                throw new ArgumentException(nameof(license));
            }
            license.IsActive = isActive;
            return await _licenseRepository.UpdateAsync(license);
        }
        public async Task<bool> ActivateLicenseAsync(int id)
        {
            return await _UpdateLicenseStatus(id, true);
        }

        public async Task<bool> DeActivateLicenseAsync(int id)
        {
            return await _UpdateLicenseStatus(id, false);
        }

        public async Task<bool> DeleteLicenseAsync(int id)
        {
           if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            return await _licenseRepository.DeleteAsync(id);
        }

        public async Task<ReadLicenseDTO?> FindByIDAsync(int id)
        {
           if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var license = await _licenseRepository.FindByIDAsync(id);
           return license is null ? null : _mapper.Map<ReadLicenseDTO>(license);
        }

        public async Task<IEnumerable<ReadLicenseDTO>> GetAllLicenseAsync()
        {
            var licenses = await _licenseRepository.GetAllAsync();
            if (licenses is null || !licenses.Any())
            {
                return Enumerable.Empty<ReadLicenseDTO>();
            }
            return _mapper.Map<IEnumerable<ReadLicenseDTO>>(licenses);
        }

        public async Task<bool> IsLicenseActive(int id)
        {
           return await _licenseRepository.IsLicenseActiveAsync(id);
        }

        public async Task<int> IssueDrivingLicense(LicenseDTO licenseDTO,
            Enums.IssueReason issueReason = Enums.IssueReason.FirstTime)
        {
           if(licenseDTO is null)
            {
                throw new ArgumentNullException(nameof(licenseDTO), "License DTO cannot be null.");
            }
           if(!(await _applicationService.IsExistAsync(licenseDTO.ApplicationID)))
            {
                throw new ArgumentException("Application does not exist.", nameof(licenseDTO.ApplicationID));
            }
            // to do later add validation for DriverID if it exists in the system
            var license = _mapper.Map<License>(licenseDTO);

            if(await _applicationService.CompleteApplicationAsync(licenseDTO.ApplicationID) is false)
            {
                throw new InvalidOperationException("Application could not be completed.");
            }

            license.IssueDate = DateTime.UtcNow;
            var licenseValidityLength = await _licenseClassService
                .GetLicenseValidityLengthAsync(license.LicenseClass);
            license.ExpirationDate = license.IssueDate.AddYears(licenseValidityLength);
            license.IsActive = true;
            license.IssueReason = (byte)issueReason;   
            return await _licenseRepository.AddAsync(license);
        }

        public async Task<bool> IsLicenseExpired(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var license = await _licenseRepository.FindByIDAsync(id);
            if (license is null)
            {
                throw new ArgumentException(nameof(license));
            }
            int result = DateTime.Compare(license.ExpirationDate, license.IssueDate);
            return result < 0 ? true : false;
        }

        public async Task<bool> IsLicenseExistAndActiveAsync(int id)
        {
            if(id<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            return await _licenseRepository.IsExistAndActiveAsync(id);
        }
    }
}
