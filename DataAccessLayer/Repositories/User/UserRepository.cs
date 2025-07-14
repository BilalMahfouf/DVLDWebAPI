using Core.Interfaces.Repositories.Users;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.User
{
    public class UserRepository : Repository<DataAccessLayer.User>, IUserRepository
    {
        public UserRepository(DvldDBContext context) : base(context)
        {
        }
        public async Task<bool> isExistByPersonID(int personID)
        {
            return await _context.Users
                .AnyAsync(u => u.PersonID == personID); 
        }
    }
}
