using Core.DTOs.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Tests
{
    public interface ITestAppointmentService
    {
        Task<TestAppointmentDTO?> FindByIDAsync(int id);
        Task<int> CreateTestAppointmentAsync(TestAppointmentDTO testAppointmentDTO);
        Task<bool> UpdateTestAppointmentAsync(int id,int retakeTestAppointmentID);
        Task<bool> DeleteTestAppointmentAsync(int id);
        Task<bool> LockTestAppointment(int id);
        Task<IEnumerable<TestAppointmentDTO>> GetAllTestAppointmentAsync();

    }
}
