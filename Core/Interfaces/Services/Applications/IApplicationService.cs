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
    public interface IApplicationService
    {
        Task<GenericResult<int>> CreateApplicationAsync(ApplicationDTO application,
            Enums.ApplicationTypeEnum applicationType
                = Enums.ApplicationTypeEnum.NewLocalDrivingLicense);
    Task<Result> DeleteApplicationAsync(int applicationID);
    Task<GenericResult<ReadApplicationDTO>> FindByIDAsync(int applicationID);
    Task<Result> CancelApplication(int  applicationID);
    Task<Result> CompleteApplicationAsync(int applicationID);
    
    }
}
