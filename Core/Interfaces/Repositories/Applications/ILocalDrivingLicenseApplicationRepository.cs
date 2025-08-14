using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Applications
{
    public interface ILocalDrivingLicenseApplicationRepository:
        IRepository<LocalDrivingLicenseApplication>
    {
        Task<IEnumerable<LocalDrivingLicenseApplications_View>> GetAll_ViewAsync();
        Task<bool> IsExistNewAppAsync(int personID, int licenseClassID);
    }
}
