using Core.DTOs.License;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface IInternationalLicenseService
    {
        Task<GenericResult<int>> IssueInternationalLicense
            (InternationalLicenseDTO licenseDTO);
        Task<Result> DeleteInternationalLicenseAsync(int id);
        Task<GenericResult<ReadInternationalLicenseDTO>> FindByIDAsync(int id);
        Task<GenericResult<IEnumerable<ReadInternationalLicenseDTO>>> GetAllAsync();
        Task<Result> ActivateAsync(int id);
        Task<Result> DeActivateAsync(int id);

    }
}
