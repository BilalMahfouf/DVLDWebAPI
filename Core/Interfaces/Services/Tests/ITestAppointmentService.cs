using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Tests
{
    public interface ITestAppointmentService<T> where T : class
    {
        Task<T?> FindByIDAsync(int id);
        Task<int> CreateTestAppointmentAsync(T testAppointmentDTO);
        Task<bool> UpdateTestAppointmentAsync(int id,T testAppointmentDTO);
        Task<bool> DeleteTestAppointmentAsync(int id);
        Task<bool> LockTestAppointment(int id);
        Task<IEnumerable<T>> GetAllTestAppointmentAsync();

    }
}
