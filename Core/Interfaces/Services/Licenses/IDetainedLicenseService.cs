using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Licenses
{
    public interface IDetainedLicenseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindAsync(int id);
        Task<int> CreateDetainedLicenseAsync(T detainedLicenseDTO);
        Task<bool> ReleaseLicenseAsync(T releaseDTO);
        Task<bool> DeleteDetainedLicenseAsync(int id);
    }
}
