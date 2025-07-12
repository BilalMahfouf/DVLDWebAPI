using AutoMapper;
using Core.DTOs.Application;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services
{
    public class ApplicationTypeService
    {
        private readonly IReadUpdateRepository<ApplicationType> _repo;
        private readonly IMapper _mapper;

        public ApplicationTypeService(IMapper mapper, IReadUpdateRepository<ApplicationType> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<IEnumerable<ReadApplicationDTO>> GetAllApplicationTypesAsync()
        {
            var applicationTypes = await _repo.GetAllAsync();
            var readApplicationTypesDTO = _mapper.Map<IEnumerable<ReadApplicationDTO>>(applicationTypes);
            return readApplicationTypesDTO;
        }
    
        public async Task<bool> UpdateFeesAsync(int applicationTypeID,decimal fees)
        {
            if (applicationTypeID <= 0) 
            {
                throw new ArgumentNullException(nameof(applicationTypeID), "Application type ID must be greater than zero.");
            }
            if(fees < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fees), "Fees cannot be negative.");
            }
            var applicationType = await _repo.FindByIDAsync(applicationTypeID);
            if (applicationType is null)
            {
              throw new ArgumentNullException(nameof(applicationType), "Application type not found.");
            }
            applicationType.ApplicationFees = fees;
            return await _repo.UpdateAsync(applicationType);
        }
    
        public async Task<ReadApplicationDTO?> FindByIDAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var applicationType = await _repo.FindByIDAsync(id);
            if (applicationType is null)
            {
                return null;
            }
            return _mapper.Map<ReadApplicationDTO>(applicationType);
        }
    }
}
