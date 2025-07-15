using Core.Common;
using Core.DTOs.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface ILicenseService 
    {
        Task<ReadLicenseDTO?> FindByIDAsync(int  id);
        Task<int> IssueNewDrivingLicenseAsync(LicenseDTO licenseDTO);
        Task<int> RenewLicenseAsync(int oldLicenseID,LicenseDTO licenseDTO);
        Task<int> IssueReplacementForLostLicenseAsync(int oldLicenseID,
            LicenseDTO licenseDTO);
        Task<int> IssueReplacementForDamagedLicenseAsync(int oldLicenseID
            , LicenseDTO licenseDTO);
        Task <bool>DeleteLicenseAsync(int id);
        Task<bool> ActivateLicenseAsync(int id);
        Task<bool> DeActivateLicenseAsync(int id);
        Task<bool> IsLicenseExpired(int id);
        Task<bool> IsLicenseActive(int id);
        Task<IEnumerable<ReadLicenseDTO>> GetAllLicenseAsync();
        Task<bool> IsLicenseExistAndActiveAsync(int id);

    }
}
