using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Licenses
{
    public interface ILicenseRepository:IRepository<License>
    {
        Task<bool> IsLicenseActiveAsync(int id);
        Task<bool> IsExistAndActiveAsync(int id);
    }
}
