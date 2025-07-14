using AutoMapper;
using Core.DTOs.License;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Licenses
{
    public class LicenseClassService
    {
        private readonly IReadUpdateRepository<LicenseClass> _repo;
        private readonly IMapper _mapper;
        public LicenseClassService(IReadUpdateRepository<LicenseClass> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LicenseClassDTO>> GetAllAsync()
        {
            var licenseClasses = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<LicenseClassDTO>>(licenseClasses);
        }
        public async Task<LicenseClassDTO?> FindByIDAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var licenseClass = await _repo.FindByIDAsync(id);
            return licenseClass is null ? null : _mapper.Map<LicenseClassDTO>(licenseClass);
        }
        public async Task<bool> UpdateFeesAsync(int licenseClassID, decimal fees)
        {
            if (licenseClassID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(licenseClassID), "License class ID must be greater than zero.");
            }
            if (fees < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fees), "Fees cannot be negative.");
            }
            var licenseClass = await _repo.FindByIDAsync(licenseClassID);
            if (licenseClass is null)
            {
                throw new ArgumentNullException(nameof(licenseClass), "License class not found.");
            }
            licenseClass.ClassFees = fees;
            return await _repo.UpdateAsync(licenseClass);
        }

        public async Task<byte> GetLicenseValidityLengthAsync(int licenseClassID)
        {
            if (licenseClassID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(licenseClassID)
                    , "license class id must be greater then 0");
            }
            var licenseClass = await _repo.FindByIDAsync(licenseClassID);
            if(licenseClass is null )
            {
                throw new InvalidOperationException(nameof(licenseClass));
            }
            if(licenseClass.DefaultValidityLength <=0)
            {
                throw new ArgumentOutOfRangeException
                    (nameof(licenseClass.DefaultValidityLength));
            }
            return licenseClass.DefaultValidityLength;
            
        }
    }
}
