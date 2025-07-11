using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface ILicenseService<T> where T : class
    {
        Task<T?> FindByIDAsync(int  id);
        Task<int> IssueDrivingLicense(T licenseDTO);
        Task<bool>UpdateLicenseAsync(T licenseDTO);
        Task DeleteLicenseAsync(int id);
        Task<bool> ActivateLicenseAsync(int id);
        Task<bool> DeActivateLicenseAsync(int id);
        Task<bool> IsLicenseExpired(int id);
        Task<bool> IsLicenseActive(int id);
        Task<IEnumerable<T>> GetAllLicenseAsync();

    }
}
