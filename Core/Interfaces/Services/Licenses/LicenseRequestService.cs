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
    public interface ILicenseRequestService
    {
        Task<GenericResult<int>> IssueNewDrivingLicenseAsync(LicenseDTO licenseDTO);
        Task<GenericResult<int>> RenewLicenseAsync(int oldLicenseID, LicenseDTO licenseDTO);
        Task<GenericResult<int>> IssueReplacementForLostLicenseAsync(int oldLicenseID
            , LicenseDTO licenseDTO);
        Task<GenericResult<int>> IssueReplacementForDamagedLicenseAsync(int oldLicenseID
        , LicenseDTO licenseDTO);
    }
}
