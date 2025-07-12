using Core.DTOs.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Drivers
{
    public interface IDriverService<DriverDTO> where DriverDTO : class
    {
        Task<DriverDTO?> FindByIDAsync(int id);
        Task<int> CreateDriverAsync(DriverDTO driverDTO);
        Task<IEnumerable<DriverDashboardDTO>> GetAllDriversAsync();

    }
}
