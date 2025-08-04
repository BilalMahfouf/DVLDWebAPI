using AutoMapper;
using Core.Common;
using Core.DTOs.License;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Shared;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public LicenseClassService( IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<GenericResult<IEnumerable<LicenseClassDTO>>> GetAllAsync()
        {
            try
            {
                var licenseClasses = await _uow.applicationTypeRepository.GetAllAsync();
                if(licenseClasses is null || !licenseClasses.Any())
                {
                    return GenericResult<IEnumerable<LicenseClassDTO>>
                        .Failure("No license classes found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<IEnumerable<LicenseClassDTO>>.Success
                    (_mapper.Map<IEnumerable<LicenseClassDTO>>(licenseClasses));
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<LicenseClassDTO>>
                    .Failure($"An error occurred while retrieving data from the DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }
        public async Task<GenericResult<LicenseClassDTO?>> FindByIDAsync(int id)
        {
            if (id <= 0)
            {
                return GenericResult<LicenseClassDTO?>
                    .Failure("License class ID must be greater than zero.", Enums.ErrorType.BadRequest);
            }
            try
            {
                var licenseClass = await _uow.applicationTypeRepository.FindAsync
               (a => a.ApplicationTypeID == id);
                if (licenseClass is null)
                {
                    return GenericResult<LicenseClassDTO?>
                        .Failure("License class not found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<LicenseClassDTO?>.Success
                    (_mapper.Map<LicenseClassDTO>(licenseClass));
            }
            catch (Exception ex)
            {
                return GenericResult<LicenseClassDTO?>
                    .Failure($"An error occurred while retrieving data from the DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }
        public async Task<Result> UpdateFeesAsync(int licenseClassID, decimal fees)
        {
            if (licenseClassID <= 0 || fees < 0)
            {
                Result.Failure("License class id must be greater than zero and fees cannot be negative."
                    , Enums.ErrorType.BadRequest);
            }
            try
            {
                var licenseClass = await _uow.applicationTypeRepository
                    .FindAsync(a => a.ApplicationTypeID == licenseClassID);
                if (licenseClass is null)
                {
                    return Result.Failure("License class not found.", Enums.ErrorType.NotFound);
                }
                licenseClass.ApplicationFees = fees;
                _uow.applicationTypeRepository.Update(licenseClass);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to update license class fees."
                    , Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while saving data to the DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

       
    }
}
