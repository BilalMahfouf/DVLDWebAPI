using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Common
{
    public  class ReadUpdateRepository<TEntity> : IReadUpdateRepository<TEntity>
        where TEntity : class,IEntity
    {
        protected readonly DvldDBContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public ReadUpdateRepository(DvldDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync
        (Expression<Func<TEntity, bool>> filter = null!, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            return await  query.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
        public virtual async Task<TEntity?> FindAsync
         (Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
           IQueryable<TEntity> query = _dbSet;
          
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(filter);
        }
    }
}
