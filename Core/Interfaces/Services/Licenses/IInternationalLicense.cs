using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface IInternationalLicense< T> where T : class
    {
        Task<int> IssueInternationalLicense(T licenseDTO);
        Task<bool> UpdateInternationalLicenseAsync(T licenseDTO);
        Task<bool> DeleteInternationalLicenseAsync(int licenseID);
        Task<T?> FindByIDAsync(int licenseID);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> ActivateAsync(int licenseID);
        Task<bool> DeActivateAsync(int licenseID);

    }
}
