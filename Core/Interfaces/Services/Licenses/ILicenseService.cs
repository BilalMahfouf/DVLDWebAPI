using Core.Common;
using Core.DTOs.License;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface ILicenseService 
    {
        Task<GenericResult<ReadLicenseDTO?>> FindByIDAsync(int  id);
        Task<GenericResult<int>> IssueNewDrivingLicenseAsync(LicenseDTO licenseDTO);
        Task<GenericResult<int>> RenewLicenseAsync(int oldLicenseID,LicenseDTO licenseDTO);
        Task<GenericResult<int>> IssueReplacementForLostLicenseAsync(int oldLicenseID,
            LicenseDTO licenseDTO);
        Task<GenericResult<int>> IssueReplacementForDamagedLicenseAsync(int oldLicenseID
            , LicenseDTO licenseDTO);
        Task <Result>DeleteLicenseAsync(int id);
        Task<Result> ActivateLicenseAsync(int id);
        Task<Result> DeActivateLicenseAsync(int id);
        Task<GenericResult<IEnumerable<ReadLicenseDTO>>> GetAllLicenseAsync();

    }
}
