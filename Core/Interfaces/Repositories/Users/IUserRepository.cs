using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Users
{
    public interface IUserRepository:IRepository<User>
    {
        Task<bool> isExistByPersonID(int personID);
    }
}
