using Core.DTOs.Detain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface IDetainedLicenseService
    {
        Task<IEnumerable<DetainLicenseDTO>> GetAllAsync();
        Task<DetainLicenseDTO?> FindAsync(int id);
        Task<int> CreateDetainedLicenseAsync(DetainLicenseDTO detainedLicenseDTO);
        Task<bool> ReleaseLicenseAsync(UpdateDetainedLicenseDTO releaseDTO);
        Task<bool> DeleteDetainedLicenseAsync(int id);
    }
}
