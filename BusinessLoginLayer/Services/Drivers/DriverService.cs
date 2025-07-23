using AutoMapper;
using Core.DTOs.Driver;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Drivers;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Drivers
{
    public class DriverService : IDriverService
    {
        private readonly IRepository<Driver> _repo;
        private readonly IMapper _mapper;

        public DriverService(IMapper mapper, IRepository<Driver> driverRepository)
        {
            _mapper = mapper;
            _repo = driverRepository;
        }

        public async Task<int> CreateDriverAsync(DriverDTO driverDTO)
        {
            if(driverDTO is null)
            {
                throw new ArgumentNullException(nameof(driverDTO), "Driver DTO cannot be null.");
            }
            var driver= _mapper.Map<Driver>(driverDTO);
            driver.CreatedDate = DateTime.UtcNow;
            return await _repo.AddAsync(driver);
        }

        public async Task<ReadDriverDTO?> FindByIDAsync(int id)
        {
           if(id<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
           var driver = await _repo.FindAsync(id);
            return driver is null ? null : _mapper.Map<ReadDriverDTO>(driver);
        }

        public async Task<IEnumerable<DriverDashboardDTO>> GetAllDriversAsync()
        {
           var drivers = await _repo.GetAllAsync();
           if(drivers is null || !drivers.Any())
            {
                return Enumerable.Empty<DriverDashboardDTO>();
            }
              return _mapper.Map<IEnumerable<DriverDashboardDTO>>(drivers);
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            return await _repo.DeleteAsync(id);
        }
    }
}
