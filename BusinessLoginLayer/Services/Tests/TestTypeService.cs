using AutoMapper;
using Core.Common;
using Core.DTOs.Test;
using Core.Interfaces;
using Core.Interfaces.Repositories.Common;
using Core.Shared;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Tests
{
    public class TestTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public TestTypeService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<GenericResult<IEnumerable<TestTypeDTO>>> GetAllAsync()
        {
           try
            {
                var testTypes = await _uow.testTypeRepository.GetAllAsync();
                if(testTypes is null || !testTypes.Any())
                {
                    return GenericResult<IEnumerable<TestTypeDTO>>
                        .Failure("No test types found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<IEnumerable<TestTypeDTO>>.Success
                    (_mapper.Map<IEnumerable<TestTypeDTO>>(testTypes));
            }
            catch (Exception ex)
            {
                return GenericResult<IEnumerable<TestTypeDTO>>
                    .Failure($"An error occurred while retrieving data from the DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }
        }

        public async Task<GenericResult<TestTypeDTO>> FindByIDAsync(int id)
        {
            if (id <= 0)
            {
                return GenericResult<TestTypeDTO>
                    .Failure("Test type ID must be greater than zero."
                    , Enums.ErrorType.BadRequest);
            }
            try
            {
                var testType = await _uow.testTypeRepository.FindAsync(t => t.TestTypeID == id);
                if (testType is null)
                {
                    return GenericResult<TestTypeDTO>
                        .Failure("Test type not found.", Enums.ErrorType.NotFound);
                }
                return GenericResult<TestTypeDTO>.Success
                          (_mapper.Map<TestTypeDTO>(testType));
            }
            catch(Exception ex)
            {
                return GenericResult<TestTypeDTO>
                    .Failure($"An error occurred while retrieving data from the DB: " +
                    $"{ex.Message}", Enums.ErrorType.InternalServerError);
            }
           
        }

        public async Task<Result>UpdateFeesAsync(int testTypeID, decimal fees)
        {
            if (testTypeID <= 0 || fees < 0)
            {
                return Result.Failure("Test type ID must be greater than zero. " +
                    "Fees must be greater than or equal to zero.", Enums.ErrorType.BadRequest);
            }
           try
            {
                var feesToUpdate = await _uow.testTypeRepository.FindAsync(t => t.TestTypeID == testTypeID);
                if(feesToUpdate is null)
                {
                    return Result.Failure("Test type not found.", Enums.ErrorType.NotFound);
                }
                feesToUpdate.TestTypeFees = fees;
                _uow.testTypeRepository.Update(feesToUpdate);
                var result = await _uow.SaveChangesAsync();
                if(result)
                {
                    return Result.Success;
                }
                return Result.Failure("Failed to update test type fees.", 
                    Enums.ErrorType.Conflict);
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while saving data to the DB: {ex.Message}", Enums.ErrorType.InternalServerError);
            }

        }
    }
}
