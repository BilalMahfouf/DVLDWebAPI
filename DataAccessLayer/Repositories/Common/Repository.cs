using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Common
{
    public class Repository<TEntity> : ReadUpdateRepository<TEntity>, IRepository<TEntity>
        where TEntity : class,IEntity
    {
        public Repository(DvldDBContext dbContext):base(dbContext) { }
        public async Task<int> AddAsync(TEntity entity)
        {
            int insertedID = 0;
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                 insertedID=entity.ID;
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
            return insertedID;
        }

        public async Task<bool> DeleteAsync(int ID)
        {
           if(ID == 0)
            {
                throw new ArgumentNullException(nameof(ID));
            }
            bool result = false;
           try
            {
                var entity=await FindByIDAsync(ID);
                if (entity is null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _context.Remove(entity);
                result = await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw new Exception(nameof(ex));
            }
            return result;
        }

        
    }
}
