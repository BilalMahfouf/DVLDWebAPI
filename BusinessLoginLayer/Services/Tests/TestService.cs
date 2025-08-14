using AutoMapper;
using Core.Common;
using Core.DTOs.Test;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Tests;
using Core.Shared;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Tests
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TestService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<GenericResult<int>> CreateTestAsync(TestDTO testDTO)
        {
            if(testDTO is null)
            {
                return GenericResult<int>.Failure("Test data cannot be null.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var newTest = _mapper.Map<Test>(testDTO);
                _uow.testRepository.Add(newTest);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return GenericResult<int>.Success(newTest.TestID);  
                }
                return GenericResult<int>.Failure("Failed to create test.",
                    Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return GenericResult<int>.Failure($"An error occurred while saving data to the DB: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<Result> DeleteTestAsync(int id)
        {
           if(id <= 0)
            {
                return Result.Failure("ID must be greater than zero.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                if(!(await _uow.testRepository.IsExistAsync(t => t.TestID == id)))
                {
                    return Result.Failure("Test not found", Enums.ErrorType.NotFound);
                }
                _uow.testRepository.Delete(id);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to delete test", Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while saving data to the DB: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<TestDTO>> FindByIDAsync(int id)
        {
            if (id <= 0)
            {
                               return GenericResult<TestDTO>.Failure("ID must be greater than zero.",
                    Enums.ErrorType.BadRequest);
            }
            try
            {
                var test = await _uow.testRepository.FindAsync(t => t.TestID == id);
                if(test is null)
                {
                    return GenericResult<TestDTO>.Failure("Test not found.",
                        Enums.ErrorType.NotFound);
                }
                return GenericResult<TestDTO>.Success(_mapper.Map<TestDTO>(test));
            }
            catch (Exception ex)
            {
                return GenericResult<TestDTO>.Failure($"An error occurred while retrieving data from the DB: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }
        }

    }
}
