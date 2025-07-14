using Core.Interfaces.Repositories.People;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Person
{
    public class PersonRepository : Repository<DataAccessLayer.Person>, IPersonRepository
    {
        
        public PersonRepository(DvldDBContext context) : base(context)
        {
        }
        public async Task<DataAccessLayer.Person?> FindByNationalNoAsync(string NationalNo)
        {
            return await _context.People
                 .FirstOrDefaultAsync(p => p.NationalNo == NationalNo);
        }

        public async Task<bool> IsExistAsync(string NationalNo)
        {
            return await _context.People.AnyAsync(p => p.NationalNo == NationalNo);
        }

        public async Task<bool> IsExistAsync(int ID)
        {
            return await _context.People.AnyAsync(p => p.PersonID == ID);
        }
    }
}
