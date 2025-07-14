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
    public class ApplicationRepository : Repository<DataAccessLayer.Application>, IApplicationRepository
    {
        public ApplicationRepository(DvldDBContext context) : base(context)
        {
        }
        public Task<bool> IsExistAsync(int applicationID)
        {
            return _context.Applications.AnyAsync(a => a.ApplicationID == applicationID);
        }
    }
}
