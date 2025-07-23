using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Common
{
    public interface IRepository<TEntity> : IReadUpdateRepository<TEntity> 
        where TEntity : class, IEntity
    {
        void Add (TEntity entity);
        void Delete (int entity);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);
    }
}
