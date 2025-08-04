using Core.Common;
using Core.DTOs.Application;
using Core.DTOs.Detain;
using Core.DTOs.Driver;
using Core.DTOs.License;
using Core.DTOs.User;
using Core.Interfaces;
using Core.Shared;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Helpers
{
    public static class RelationshipsValidator
    {
        public static async Task<GenericResult<bool>> ValidateForCreateUserAsync
            (this CreateUserDTO userDto, IUnitOfWork uow)
        {
            var result = await uow.userRepository.IsExistAsync(u => u.PersonID == userDto.PersonID);
            return result ? GenericResult<bool>.Failure
                ("this user already exist", Enums.ErrorType.Conflict) : GenericResult<bool>.Success(true);
        }
        public static async Task<Result> ValidateForCreateLicenseAsync
            (this LicenseDTO licenseDto, IUnitOfWork uow)
        {
            if (licenseDto is null)
            {
                return Result.Failure("License data is null", Enums.ErrorType.BadRequest);
            }
            if (!(await uow.applicationRepository.IsExistAsync
                (a => a.ApplicationID == licenseDto.ApplicationID)))
            {
                return Result.Failure("Application does not exist"
                    , Enums.ErrorType.BadRequest);
            }
            if (licenseDto.PaidFees < 0)
            {
                return Result.Failure("Paid fees is negative", Enums.ErrorType.BadRequest);
            }
            if (!(await uow.driverRepository.IsExistAsync
                (d => d.DriverID == licenseDto.DriverID)))
            {
                return Result.Failure("Driver does not exist"
                    , Enums.ErrorType.BadRequest);
            }
            if (!(await uow.licenseClassRepository.IsExistAsync
                (lc => lc.LicenseClassID == (int)licenseDto.LicenseClass)))
            {
                return Result.Failure("License class does not exist"
                    , Enums.ErrorType.BadRequest);
            }
            return Result.Success;
        }

        public static async Task<Result> ValidateForCreateLocalDrivingLicenseApplicationAsync
            (this LocalDrivingLicenseDTO localDLAppDto, IUnitOfWork uow)
        {
            if (localDLAppDto is null)
            {
                return Result.Failure("Local Driving License application data is null", Enums.ErrorType.BadRequest);
            }
            var application = await uow.applicationRepository.
                FindAsync(a => a.ApplicationID == localDLAppDto.ApplicationID);
            if (application is null)
            {
                return Result.Failure("Application does not exist", Enums.ErrorType.BadRequest);
            }

            if (!(await uow.licenseClassRepository.IsExistAsync
                (lc => lc.LicenseClassID == (int)localDLAppDto.LicenseClassID)))
            {
                return Result.Failure("License class does not exist", Enums.ErrorType.BadRequest);
            }

            if (!(await uow.localDrivingLicenseApplicationRepository.IsExistNewAppAsync
                (application.ApplicantPersonID, (int)localDLAppDto.LicenseClassID)))
            {
                return Result.Failure($"Person with id {application.ApplicantPersonID}" +
                    " has already a new and active LocalDrivingLicenseApplication"
                    , Enums.ErrorType.Conflict);
            }

            return Result.Success;
        }

        public static async Task<Result> ValidateForCreateDriverAsync(this DriverDTO driverDto, IUnitOfWork uow)
        {
            if (driverDto is null)
            {
                return Result.Failure("Driver data is null", Enums.ErrorType.BadRequest);
            }
            if (!(await uow.personRepository.IsExistAsync(p => p.PersonID == driverDto.PersonID)))
            {
                return Result.Failure("Person does not exist", Enums.ErrorType.BadRequest);
            }
            return Result.Success;

        }
            
        private static async Task<bool> IsExistActiveNotExpiredLicense(int id
            , IUnitOfWork uow)
        {
            return await uow.licenseRepository.IsExistAsync(l =>
                l.LicenseID == id && l.IsActive && l.ExpirationDate > DateTime.UtcNow);
        }
        public static async Task<Result> ValidateForCreateInternationalLicenseAsync
            (this InternationalLicenseDTO internationalLicenseDto, IUnitOfWork uow)
        {
            if (internationalLicenseDto is null)
            {
                return Result.Failure("International License data is null", Enums.ErrorType.BadRequest);
            }
            if (!(await uow.applicationRepository.IsExistAsync
                (a => a.ApplicationID == internationalLicenseDto.ApplicationID)))
            {
                return Result.Failure("Application does not exist", Enums.ErrorType.BadRequest);
            }
            if (!(await uow.driverRepository.IsExistAsync
                (d => d.DriverID == internationalLicenseDto.DriverID)))
            {
                return Result.Failure("Driver does not exist", Enums.ErrorType.BadRequest);
            }
              bool hasValidLocalLicense = await IsExistActiveNotExpiredLicense
                (internationalLicenseDto.IssuedUsingLocalLicenseID, uow);

            if (!hasValidLocalLicense)
            {
                return Result.Failure(
                    "Local license does not exist, is not active, or is expired.",
                    Enums.ErrorType.BadRequest);
            }
            return Result.Success;
        }

        public static async Task<Result> ValidateForCreateDetainedLicenseAsync
            (this DetainLicenseDTO detainedLicenseDto, IUnitOfWork uow)
        {
            if (detainedLicenseDto is null)
            {
                return Result.Failure("Detained License data is null", Enums.ErrorType.BadRequest);
            }
            var hasValidLocalLicense = await IsExistActiveNotExpiredLicense
                (detainedLicenseDto.LicenseID, uow);
            if (!hasValidLocalLicense)
            {
                return Result.Failure("License does not exist or is not active or expired"
                    , Enums.ErrorType.BadRequest);
            }
            return Result.Success;
        }

        public static async Task<Result> ValidateForUpdateDetainedLicenseAsync
            (this UpdateDetainedLicenseDTO updateDetainedLicenseDto, IUnitOfWork uow)
        {
            if (updateDetainedLicenseDto is null)
            {
                return Result.Failure("Update Detained License data is null", Enums.ErrorType.BadRequest);
            }
            if (updateDetainedLicenseDto.DetainID <= 0)
            {
                return Result.Failure("Invalid Detain ID", Enums.ErrorType.BadRequest);
            }
            if (!(await uow.detainedLicenseRepository.IsExistAsync
                (d => d.DetainID == updateDetainedLicenseDto.DetainID && !d.IsReleased)))
            {
                return Result.Failure("Detained License does not exist or it has been " +
                    "released", Enums.ErrorType.BadRequest);
            }
            if (!(await uow.userRepository.IsExistAsync
                (u => u.UserID == updateDetainedLicenseDto.ReleasedByUserID)))
            {
                return Result.Failure("Released By User does not exist", Enums.ErrorType.BadRequest);
            }
            if (!(await uow.applicationRepository.IsExistAsync
                (a => a.ApplicationID == updateDetainedLicenseDto.ReleaseApplicationID)))
            {
                return Result.Failure("Release Application does not exist", Enums.ErrorType.BadRequest);
            }
            return Result.Success;
        }

    }
}
