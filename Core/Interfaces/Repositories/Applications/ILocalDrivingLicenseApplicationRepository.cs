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
        Task<bool> IsExistNewAppAsync(int personID, int licenseClass);
        Task<IEnumerable<LocalDrivingLicenseApplications_View>> GetAll_ViewAsync();
    }
}
