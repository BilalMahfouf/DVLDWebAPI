using AutoMapper;
using BusinessLoginLayer.Helpers;
using Core.Common;
using Core.DTOs.Driver;
using Core.DTOs.License;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Drivers;
using Core.Shared;
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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DriverService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<GenericResult<int>> CreateDriverAsync(DriverDTO driverDTO)
        {
            try
            {
                var validationResult = await driverDTO.ValidateForCreateDriverAsync(_uow);
                if (!validationResult.IsSuccess)
                {
                    return GenericResult<int>.Failure(validationResult.ErrorMessage, validationResult.ErrorType);
                }
                var driver = _mapper.Map<Driver>(driverDTO);
                driver.CreatedDate = DateTime.UtcNow;
                _uow.driverRepository.Add(driver);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return GenericResult<int>.Success(driver.DriverID);
                }
                return GenericResult<int>.Failure("Failed to create driver"
                    , Enums.ErrorType.Conflict);
            }
            catch(Exception ex)
            {
                return GenericResult<int>.Failure($"An error occurred while saving data" +
                    $" to the DB: {ex.Message}"
                    , Enums.ErrorType.InternalServerError);
            }
        }
            

        public async Task<GenericResult<ReadDriverDTO>> FindByIDAsync(int id)
        {
           if(id <= 0)
            {
                return GenericResult<ReadDriverDTO>.Failure("ID must be greater than zero."
                    , Enums.ErrorType.BadRequest);
            }
            try
            {
                var driver = await _uow.driverRepository.FindAsync(d => d.DriverID == id);
                if (driver is null)
                {
                    return GenericResult<ReadDriverDTO>.Failure("Driver not found.", Enums
                        .ErrorType.NotFound);
                }
                return GenericResult<ReadDriverDTO>.Success(_mapper.Map<ReadDriverDTO>(driver));

            }
            catch (Exception ex)
            {
                return GenericResult<ReadDriverDTO>
                    .Failure($"An error occurred while retrieving data from the DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<IEnumerable<DriverDashboardDTO>>>
            GetAllDriversAsync()
        {
            try
            {
                IEnumerable<Driver> drivers = await _uow.driverRepository.GetAllAsync();
                if (drivers is null || !drivers.Any())
                {
                    return GenericResult<IEnumerable<DriverDashboardDTO>>
                        .Failure("No drivers found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<IEnumerable<DriverDashboardDTO>>.Success
                     (_mapper.Map<IEnumerable<DriverDashboardDTO>>(drivers));
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<DriverDashboardDTO>>
                    .Failure($"An error occurred while retrieving data from the DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<Result> DeleteDriverAsync(int id)
        {
            if(id <= 0)
            {
                return Result.Failure("invalid id", Enums.ErrorType.BadRequest);
            }
            try
            {
                _uow.driverRepository.Delete(id);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to delete driver", Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while saving data to the DB: {ex.Message}"
                    , Enums.ErrorType.InternalServerError);
            }

        }
    }
}
