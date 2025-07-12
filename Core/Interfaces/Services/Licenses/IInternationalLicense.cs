using Core.DTOs.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface IInternationalLicense
    {
        Task<int> IssueInternationalLicense(InternationalLicenseDTO licenseDTO);
        Task<bool> UpdateInternationalLicenseAsync(InternationalLicenseDTO licenseDTO);
        Task<bool> DeleteInternationalLicenseAsync(int licenseID);
        Task<InternationalLicenseDTO?> FindByIDAsync(int licenseID);
        Task<IEnumerable<InternationalLicenseDTO>> GetAllAsync();
        Task<bool> ActivateAsync(int licenseID);
        Task<bool> DeActivateAsync(int licenseID);

    }
}
