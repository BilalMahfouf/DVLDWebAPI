using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Common
{
    public  class ReadUpdateRepository<TEntity> : IReadUpdateRepository<TEntity>
        where TEntity : class,IEntity
    {
        protected readonly DvldDBContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected ReadUpdateRepository(DvldDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual  async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> query = _dbSet;
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if(entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            bool result = false;
            try
            {
                _dbSet.Update(entity);
                result= await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
            return result;
            
        }
        public virtual async Task<TEntity?> FindByIDAsync(int ID)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.ID == ID);
        }
    }
}
