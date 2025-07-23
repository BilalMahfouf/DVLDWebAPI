using Core.Interfaces.Repositories.Common;
using Core.Shared;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Applications
{
    public interface IApplicationRepository:IRepository<Application>
    {
        Task<Result<bool>> IsExistAsync(int applicationID);
    }
}
