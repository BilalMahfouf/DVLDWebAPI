using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Common
{
    public interface IRepository<TEntity> : IReadUpdateRepository<TEntity> 
        where TEntity : class, IEntity
    {
        Task<int> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(int ID);

    }
}
