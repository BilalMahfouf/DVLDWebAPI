using AutoMapper;
using Core.DTOs.Test;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services.Tests
{
    public class TestTypeService
    {
        private readonly IReadUpdateRepository<TestType> _repo;
        private readonly IMapper _mapper;
        public TestTypeService(IReadUpdateRepository<TestType> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TestTypeDTO>> GetAllAsync()
        {
            var testTypes = await _repo.GetAllAsync();
            var testTypeDTOs = _mapper.Map<IEnumerable<TestTypeDTO>>(testTypes);
            return testTypeDTOs;
        }

        public async Task<TestTypeDTO?> FindByIDAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var testType = await _repo.FindAsync(id);
            if (testType is null)
            {
                return null;
            }
            return _mapper.Map<TestTypeDTO>(testType);
        }

        public async Task<bool>UpdateFeesAsync(int testTypeID, decimal fees)
        {
            if (testTypeID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(testTypeID), "Test type ID must be greater than zero.");
            }
            if (fees < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fees), "Fees cannot be negative.");
            }
            var testType = await _repo.FindAsync(testTypeID);
            if (testType is null)
            {
                throw new ArgumentNullException(nameof(testType), "Test type not found.");
            }
            testType.TestTypeFees = fees;
            return await _repo.UpdateAsync(testType);
        }
    }
}
