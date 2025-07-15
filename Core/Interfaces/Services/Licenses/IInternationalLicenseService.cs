using Core.DTOs.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface IInternationalLicenseService
    {
        Task<int> IssueInternationalLicense(InternationalLicenseDTO licenseDTO);
        Task<bool> DeleteInternationalLicenseAsync(int licenseID);
        Task<ReadInternationalLicenseDTO?> FindByIDAsync(int licenseID);
        Task<IEnumerable<ReadInternationalLicenseDTO>> GetAllAsync();
        Task<bool> ActivateAsync(int licenseID);
        Task<bool> DeActivateAsync(int licenseID);

    }
}
