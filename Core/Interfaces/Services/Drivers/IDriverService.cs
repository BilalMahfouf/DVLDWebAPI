using Core.DTOs.Driver;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Drivers
{
    public interface IDriverService
    {
        Task<GenericResult<ReadDriverDTO>> FindByIDAsync(int id);
        Task<GenericResult<int>> CreateDriverAsync(DriverDTO driverDTO);
        Task<GenericResult<IEnumerable<DriverDashboardDTO>>> GetAllDriversAsync();
        Task<Result> DeleteDriverAsync(int id);
    }
}
