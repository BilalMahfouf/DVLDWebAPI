using AutoMapper;
using BusinessLoginLayer.Services.Applications;
using Core.DTOs.Detain;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Licenses;
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

        private readonly IRepository<DetainedLicense> _detainedLicenseRepository;
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;
        private readonly ILicenseService _licenseService;

        public DetainLicenseService(IRepository<DetainedLicense> detainLicenseRepositry, IApplicationService applicationService, IMapper mapper, ILicenseService licenseService)
        {
            _detainedLicenseRepository = detainLicenseRepositry;
            _applicationService = applicationService;
            _mapper = mapper;
            _licenseService = licenseService;
        }

        public async Task<int> CreateDetainedLicenseAsync(DetainLicenseDTO detainedLicenseDTO)
        {
            if(detainedLicenseDTO is null)
            {
                throw new ArgumentNullException(nameof(detainedLicenseDTO));
            }
            if(await _licenseService.IsLicenseExistAndActiveAsync(detainedLicenseDTO.LicenseID) is false)
            {
                throw new ArgumentException(nameof(detainedLicenseDTO.LicenseID));
            }
            
            var detainedLicense = _mapper.Map<DetainedLicense>(detainedLicenseDTO);
            detainedLicense.IsReleased = false;
            detainedLicense.DetainDate = DateTime.UtcNow;
            return await _detainedLicenseRepository.AddAsync(detainedLicense);
        }

        public async Task<bool> DeleteDetainedLicenseAsync(int id)
        {
           if(id<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            return await _detainedLicenseRepository.DeleteAsync(id);
        }

        public async Task<DetainLicenseDTO?> FindAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "id must be greater then 0");
            }
            var dLicense = await _detainedLicenseRepository.FindByIDAsync(id);
            return dLicense is null ? null : _mapper.Map<DetainLicenseDTO>(dLicense);

        }

        public async Task<IEnumerable<DetainLicenseDTO>> GetAllAsync()
        {
            var dLicenses = await _detainedLicenseRepository.GetAllAsync();
            if(dLicenses is null || !dLicenses.Any())
            {
                return Enumerable.Empty<DetainLicenseDTO>();
            }
            return _mapper.Map<IEnumerable<DetainLicenseDTO>>(dLicenses);
        }

        public async Task<bool> ReleaseLicenseAsync(UpdateDetainedLicenseDTO releaseDTO)
        {
            if(releaseDTO is null)
            {
                throw new ArgumentNullException(nameof(releaseDTO));
            }
            if(await _applicationService.IsExistAsync
                (releaseDTO.ReleaseApplicationID) is false)
            {
                throw new ArgumentException(nameof(releaseDTO.ReleaseApplicationID));
            }
            if(await _applicationService.CompleteApplicationAsync
                (releaseDTO.ReleaseApplicationID) is false)
            {
                throw new InvalidOperationException(nameof
                    (releaseDTO.ReleaseApplicationID));
            }
            var detainedLicense = await _detainedLicenseRepository.FindByIDAsync
                (releaseDTO.DetainID);
            if(detainedLicense is null )
            {
                throw new ArgumentException(nameof(detainedLicense));
            }
            detainedLicense.IsReleased = true;
            detainedLicense.ReleaseDate = DateTime.UtcNow;
            return await _detainedLicenseRepository.UpdateAsync(detainedLicense);
        }
    }
}
