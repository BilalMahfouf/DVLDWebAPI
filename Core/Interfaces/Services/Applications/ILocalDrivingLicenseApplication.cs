using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Applications
{
    public interface ILocalDrivingLicenseApplication<T> where T : class
    {
        Task<int> CreateLDLApplicationAsync(T LDLapplication);
        Task<bool> UpdateLDLApplicationAsync(int LDLapplicationID, T LDLapplication);
        Task<bool> DeleteLDLApplicationAsync(int LDLapplicationID);
        Task<T?> FindByIDAsync(int LDLapplicationID);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> CanCreateLDLApplication(int personID,
            Enums.LicenseClassTypeEnum licenseClassTypeID);
        
    }
}
