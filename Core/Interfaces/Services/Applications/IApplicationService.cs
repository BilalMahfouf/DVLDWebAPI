using Core.Common;
using Core.DTOs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Interfaces.Services.Applications
{
    public interface IApplicationService
    {
        Task<int> CreateApplicationAsync(ApplicationDTO application,
            Enums.ApplicationTypeEnum applicationType
                = Enums.ApplicationTypeEnum.NewLocalDrivingLicense
            , Enums.ApplicationStatusEnum
                applicationStatus = Enums.ApplicationStatusEnum.New);
    Task<bool> DeleteApplicationAsync(int applicationID);
    Task<ReadApplicationDTO?> FindByIDAsync(int applicationID);
    Task<bool> CancelApplication(int  applicationID);
    Task<bool> CompleteApplicationAsync(int applicationID);
    Task<bool> IsExistAsync(int applicationID);
    }
}
