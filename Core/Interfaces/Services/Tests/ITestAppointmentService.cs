using Core.DTOs.Test;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Tests
{
    public interface ITestAppointmentService
    {
        Task<GenericResult<TestAppointmentDTO>> FindByIDAsync(int id);
        Task<GenericResult<int>> CreateTestAppointmentAsync(TestAppointmentDTO testAppointmentDTO);
        Task<Result> UpdateTestAppointmentAsync(int id,int retakeTestAppointmentID);
        Task<Result> DeleteTestAppointmentAsync(int id);
        Task<Result> LockTestAppointment(int id);
        Task<GenericResult<IEnumerable<TestAppointmentDTO>>> GetAllTestAppointmentAsync();

    }
}
