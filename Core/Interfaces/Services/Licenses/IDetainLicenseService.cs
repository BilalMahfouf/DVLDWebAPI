using Core.DTOs.Detain;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface IDetainLicenseService
    {
        Task<GenericResult<IEnumerable<DetainLicenseDTO>>> GetAllAsync();
        Task<GenericResult<DetainLicenseDTO?>> FindAsync(int id);
        Task<GenericResult<int>> CreateDetainedLicenseAsync(DetainLicenseDTO detainedLicenseDTO);
        Task<Result> ReleaseLicenseAsync(UpdateDetainedLicenseDTO releaseDTO);
        Task<Result> DeleteDetainedLicenseAsync(int id);
    }
}
