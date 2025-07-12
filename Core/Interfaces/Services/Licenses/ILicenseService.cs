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
        Task<LicenseDTO?> FindByIDAsync(int  id);
        Task<int> IssueDrivingLicense(LicenseDTO licenseDTO);
        Task<bool>UpdateLicenseAsync(LicenseDTO licenseDTO);
        Task DeleteLicenseAsync(int id);
        Task<bool> ActivateLicenseAsync(int id);
        Task<bool> DeActivateLicenseAsync(int id);
        Task<bool> IsLicenseExpired(int id);
        Task<bool> IsLicenseActive(int id);
        Task<IEnumerable<LicenseDTO>> GetAllLicenseAsync();

    }
}
