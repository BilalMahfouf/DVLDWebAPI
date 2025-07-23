using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Common
{
    public class Repository<TEntity> : ReadUpdateRepository<TEntity>, IRepository<TEntity>
        where TEntity : class,IEntity
    {
        public Repository(DvldDBContext dbContext):base(dbContext) { }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public  void Delete(int id)
        {
            var entity = _dbSet.Find(id);

            _dbSet.Remove(entity);
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity,bool>> filter)
        {
        return await _dbSet.AnyAsync(filter);
        }

        
    }
}
