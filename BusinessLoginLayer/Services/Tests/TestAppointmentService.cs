using AutoMapper;
using Core.DTOs.Test;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Tests;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Tests
{
    public class TestAppointmentService : ITestAppointmentService
    {

        private readonly IRepository<TestAppointment> _repo;
        private readonly IMapper _mapper;

        public TestAppointmentService(IMapper mapper, IRepository<TestAppointment> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> CreateTestAppointmentAsync(TestAppointmentDTO testAppointmentDTO)
        {
            if(testAppointmentDTO == null)
            {
                throw new ArgumentNullException(nameof(testAppointmentDTO), "Test appointment DTO cannot be null.");
            }
            var testAppointment = _mapper.Map<TestAppointment>(testAppointmentDTO);
            return await _repo.AddAsync(testAppointment);
        }

        public async Task<bool> DeleteTestAppointmentAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            return await _repo.DeleteAsync(id);
        }

        public async Task<TestAppointmentDTO?> FindByIDAsync(int id)
        {
           if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var testAppointment = await _repo.FindAsync(id);
            return testAppointment is null ? null : _mapper.Map<TestAppointmentDTO>(testAppointment);
        }

        public async Task<IEnumerable<TestAppointmentDTO>> GetAllTestAppointmentAsync()
        {
           var testAppointments= await _repo.GetAllAsync();
            if(testAppointments is null || !testAppointments.Any())
            {
                return Enumerable.Empty<TestAppointmentDTO>();
            }
            return _mapper.Map<IEnumerable<TestAppointmentDTO>>(testAppointments);
        }

        public async Task<bool> LockTestAppointment(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var testAppointment = await _repo.FindAsync(id);
            if(testAppointment is null)
            {
                throw new ArgumentNullException(nameof(testAppointment), "Test appointment not found.");
            }
            testAppointment.IsLocked = true;
            return await _repo.UpdateAsync(testAppointment);
        }

        public async Task<bool> UpdateTestAppointmentAsync(int id, int retakeTestApplicationID)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var testAppointment = await _repo.FindAsync(id);
            if (testAppointment is null)
            {
                throw new ArgumentNullException(nameof(testAppointment), "Test appointment not found.");
            }
            testAppointment.RetakeTestApplicationID = retakeTestApplicationID;
            return await _repo.UpdateAsync(testAppointment);
        }
    }
}
