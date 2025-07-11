using Core.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;

namespace Core.Interfaces.Repositories.People
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<bool> IsExist(string NationalNo);
        Task<bool> IsExist(int ID);

    }
}


