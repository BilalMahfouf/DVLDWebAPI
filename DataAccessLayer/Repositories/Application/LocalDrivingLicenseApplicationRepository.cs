using Core.Common;
using Core.Interfaces.Repositories.Applications;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Application
{
    public class LocalDrivingLicenseApplicationRepository
        : Repository<DataAccessLayer.LocalDrivingLicenseApplication>
        , ILocalDrivingLicenseApplicationRepository
    {
        public LocalDrivingLicenseApplicationRepository(DvldDBContext context)
            : base(context)
        {
        }
        public async Task<bool> IsExistNewAppAsync(int personID, int licenseClassID)
        {

            return await _context.LocalDrivingLicenseFullApplications_Views
                .AnyAsync(l => l.LicenseClassID == licenseClassID && l.ApplicantPersonID == personID
                && l.ApplicationStatus == (byte)Enums.ApplicationStatusEnum.New);
        }
        public async Task<IEnumerable<LocalDrivingLicenseApplications_View>> GetAll_ViewAsync()
        {
            return await _context.LocalDrivingLicenseApplications_Views
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
