using Core.Common;
using Core.DTOs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Applications
{
    public interface ILocalDrivingLicenseApplicationService
    {
        Task<int> CreateLDLApplicationAsync(LocalDrivingLicenseDTO LDLapplication);
        Task<bool> DeleteLDLApplicationAsync(int LDLapplicationID);
        Task<LocalDrivingLicenseDTO?> FindLDLAppByIDAsync(int LDLapplicationID);
        Task<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>> GetAllAsync();
        Task<bool> CanCreateLDLApplication(int personID,
            Enums.LicenseClassTypeEnum licenseClassID);
        
    }
}
