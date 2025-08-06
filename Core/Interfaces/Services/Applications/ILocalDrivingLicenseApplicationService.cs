using Core.Common;
using Core.DTOs.Application;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Applications
{
    public interface ILocalDrivingLicenseApplicationService
    {
        Task<GenericResult<int>> CreateLDLApplicationAsync(LocalDrivingLicenseDTO LDLapplication);
        Task<Result> DeleteLDLApplicationAsync(int LDLapplicationID);
        Task<GenericResult<LocalDrivingLicenseDTO>> FindLDLAppByIDAsync
            (int LDLapplicationID);
        Task<GenericResult<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>>>
            GetAllAsync();
        
        
    }
}
