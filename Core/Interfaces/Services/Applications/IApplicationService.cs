using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Applications
{
    public interface IApplicationService<T> where T : class
    {
    Task<int> CreateApplicationAsync(T application);
    Task<bool> UpdateApplicationAsync(int applicationID, T application);
    Task<bool> DeleteApplicationAsync(int applicationID);
    Task<T?> FindByIDAsync(int applicationID);
    Task<bool> CancelApplication(int  applicationID);
    Task<bool> CompleteApplicationAsync(int applicationID); 



    }
}
