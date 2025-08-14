using AutoMapper;
using Core.Common;
using Core.DTOs.Test;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Tests;
using Core.Shared;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TestAppointmentService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<GenericResult<int>> CreateTestAppointmentAsync(TestAppointmentDTO testAppointmentDTO)
        {
            if(testAppointmentDTO == null)
            {
                return GenericResult<int>.Failure("Test appointment data cannot be null.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var testAppointment = _mapper.Map<TestAppointment>(testAppointmentDTO);
                _uow.testAppointmentRepository.Add(testAppointment);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return GenericResult<int>.Success(testAppointment.TestAppointmentID);
                }
                return GenericResult<int>.Failure("Failed to create test appointment.",
                    Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return GenericResult<int>.Failure($"An error occurred while saving data to the DB: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<Result> DeleteTestAppointmentAsync(int id)
        {
            if (id <= 0)
            {
                return Result.Failure("ID must be greater than zero.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                if (!(await _uow.testAppointmentRepository.IsExistAsync(d => d.TestAppointmentID == id)))
                {
                    return Result.Failure("Test appointment not found.",
                        Enums.ErrorType.NotFound);
                }
                _uow.testAppointmentRepository.Delete(id);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to delete test appointment.",
                    Enums.ErrorType.Conflict);
            }
            catch(Exception ex)
            {
                return Result.Failure($"An error occurred while saving data to the DB: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }
           }

        public async Task<GenericResult<TestAppointmentDTO>> FindByIDAsync(int id)
        {
            if (id <= 0)
            {
                return GenericResult<TestAppointmentDTO>.Failure("ID must be greater than zero.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var testAppointment = await _uow.testAppointmentRepository.FindAsync(t=>t.TestAppointmentID==id);
                if (testAppointment is null)
                {
                    return GenericResult<TestAppointmentDTO>.Failure("Test appointment not found.",
                        Enums.ErrorType.NotFound);
                }
                return GenericResult<TestAppointmentDTO>.Success(_mapper.Map<TestAppointmentDTO>(testAppointment));
            }
            catch (Exception ex)
            {
                return GenericResult<TestAppointmentDTO>.Failure($"An error occurred while retrieving data: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<IEnumerable<TestAppointmentDTO>>> GetAllTestAppointmentAsync()
        {
            try
            {
                var testAppointments = await _uow.testAppointmentRepository.GetAllAsync();
                if (testAppointments is null || !testAppointments.Any())
                {
                    GenericResult<IEnumerable<TestAppointmentDTO>>.Failure("No test appointments found.",
                        Enums.ErrorType.NotFound);
                }
                return GenericResult<IEnumerable<TestAppointmentDTO>>.Success
                    (_mapper.Map<IEnumerable<TestAppointmentDTO>>(testAppointments));
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving data from DB: {ex.Message}");
            }
           
        }

        public async Task<Result> LockTestAppointment(int id)
        {
            if(id <= 0)
            {
                return Result.Failure("ID must be greater than zero.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var testAppointment = await _uow.testAppointmentRepository.FindAsync
                    (t=>t.TestAppointmentID==id);
                if(testAppointment is null)
                {
                    return Result.Failure("Test appointment not found.",
                        Enums.ErrorType.NotFound);
                }
                testAppointment.IsLocked = true;
                _uow.testAppointmentRepository.Update(testAppointment);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to lock test appointment.",
                    Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure("An error occurred while saving data " +
                    $"to the DB ex {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<Result> UpdateTestAppointmentAsync(int id, int retakeTestApplicationID)
        {
            if (id <= 0 || retakeTestApplicationID <= 0)
            {
                return  Result.Failure("ID and Retake Test Application ID must be greater than zero.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var testAppointment = await _uow.testAppointmentRepository.FindAsync
                (t => t.TestAppointmentID == id);
                if (testAppointment is null)
                {
                    return Result.Failure("Test appointment not found.",
                        Enums.ErrorType.NotFound);
                }
                testAppointment.RetakeTestApplicationID = retakeTestApplicationID;
                _uow.testAppointmentRepository.Update(testAppointment);
                var result = await _uow.SaveChangesAsync();
                if (result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to update test appointment.",
                    Enums.ErrorType.Conflict);
            }
            catch(Exception ex)
            {
                return Result.Failure($"An error occurred while saving data to the DB: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }

        }
    }
}
