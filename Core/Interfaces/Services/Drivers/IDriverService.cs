using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Drivers
{
    public interface IDriverService<T> where T : class
    {
        Task<T?> FindByIDAsync(int id);
        Task<int> CreateDriverAsync(T driverDTO);
        Task<IEnumerable<T>> GetAllDriversAsync();

    }
}
