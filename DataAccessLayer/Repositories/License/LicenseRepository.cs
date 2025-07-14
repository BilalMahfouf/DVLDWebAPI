using Core.Interfaces.Repositories.Licenses;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.License
{
    public class LicenseRepository : Repository<DataAccessLayer.License>, ILicenseRepository
    {
        public LicenseRepository(DvldDBContext context) : base(context)
        {
        }

        public async Task<bool> IsLicenseActiveAsync(int id)
        {
            return await _context.Licenses
                .AnyAsync(l => l.LicenseID == id && l.IsActive);
        }
        public override async Task<IEnumerable<DataAccessLayer.License>> GetAllAsync()
        {
            return await _context.Licenses.Include(l => l.LicenseClassNavigation)
                .ToListAsync();
        }

       public async Task<bool> IsExistAndActiveAsync(int id)
        {
            return await _context.Licenses.AnyAsync(l=> l.LicenseID == id && l.IsActive);
        }
    }
}
