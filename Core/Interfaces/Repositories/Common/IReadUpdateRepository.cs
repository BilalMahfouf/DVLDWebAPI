using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Common
{
    public interface IReadUpdateRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter=null!, string
            includeProperties = "");
        void  Update(TEntity entity);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> filter, string
            includeProperties = "");
    }
}
