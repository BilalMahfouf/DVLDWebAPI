using AutoMapper;
using Core.DTOs.License;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Licenses;
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
        private readonly IRepository<InternationalLicense> _internationalLicenseRepository;
        private readonly ILicenseService _licenseService;
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public InternationalLicenseService(IMapper mapper,
            IRepository<InternationalLicense> internationalLicenseRepository
            , ILicenseService licenseService, IApplicationService applicationService)
        {
            _mapper = mapper;
            _internationalLicenseRepository = internationalLicenseRepository;
            _licenseService = licenseService;
            _applicationService = applicationService;
        }

        private async Task<bool> _UpdateStatus(int licenseID, bool isActive)
        {
           if(licenseID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(licenseID));
            }
           var internationalLicense=await _internationalLicenseRepository.FindByIDAsync(licenseID);
            if (internationalLicense is null)
            {
                throw new ArgumentException(nameof(internationalLicense));
            }
            internationalLicense.IsActive = isActive;
            return await _internationalLicenseRepository.UpdateAsync(internationalLicense);
        }

        public async Task<bool> ActivateAsync(int licenseID)
        {
            return await _UpdateStatus(licenseID, true);
        }

        public async Task<bool> DeActivateAsync(int licenseID)
        {
            return await _UpdateStatus(licenseID,false);
        }

        public async Task<bool> DeleteInternationalLicenseAsync(int licenseID)
        {
           if(licenseID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(licenseID));
            }
           return await _internationalLicenseRepository.DeleteAsync(licenseID);
        }

        public async Task<ReadInternationalLicenseDTO?> FindByIDAsync(int licenseID)
        {
           if(licenseID <=0)
            {
                throw new ArgumentOutOfRangeException(nameof(licenseID));
            }
            var license = await _internationalLicenseRepository.FindByIDAsync(licenseID);
            return license is null ? null : _mapper.Map<ReadInternationalLicenseDTO>(license);
        }

        public async Task<IEnumerable<ReadInternationalLicenseDTO>> GetAllAsync()
        {
            var licenses = await _internationalLicenseRepository.GetAllAsync();
            if(licenses is null || !licenses.Any())
            {
                return Enumerable.Empty<ReadInternationalLicenseDTO>();
            }
            return _mapper.Map<IEnumerable<ReadInternationalLicenseDTO>>(licenses);
        }

        public async Task<int> IssueInternationalLicense(InternationalLicenseDTO licenseDTO)
        {
           if(licenseDTO is null)
            {
                throw new ArgumentNullException(nameof(licenseDTO));
            }
           if(await _applicationService.IsExistAsync(licenseDTO.ApplicationID) is false)
            {
                throw new ArgumentException("Application does not exist.", nameof(licenseDTO.ApplicationID));
            }
            if (await _licenseService.IsLicenseExistAndActiveAsync
                (licenseDTO.IssuedUsingLocalLicenseID) is false) 
            {
                throw new ArgumentException("License does not exist or is not active."
                    , nameof(licenseDTO.IssuedUsingLocalLicenseID));
            }
            if (await _applicationService.CompleteApplicationAsync
                (licenseDTO.ApplicationID) is false) 
            {
                throw new InvalidOperationException("Application could not be completed.");
            }
            var license = _mapper.Map<InternationalLicense>(licenseDTO);
            license.IsActive = true;
            license.IssueDate=DateTime.UtcNow;
            // Assuming 5 years validity
            license.ExpirationDate = license.IssueDate.AddYears(5);
            return await _internationalLicenseRepository.AddAsync(license);
        }

    }
}
