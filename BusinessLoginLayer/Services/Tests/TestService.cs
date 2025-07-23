using AutoMapper;
using Core.Common;
using Core.DTOs.Test;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Services.Tests;
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
        private readonly IRepository<Test> _repo;
        private readonly IMapper _mapper;

        public TestService(IRepository<Test> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> CreateTestAsync(TestDTO testDTO)
        {
            if(testDTO is null)
            {
                throw new ArgumentNullException(nameof(testDTO));
            }
            var newTest = _mapper.Map<Test>(testDTO);
            var insertedID= await _repo.AddAsync(newTest);
            return insertedID;
        }

        public async Task<bool> DeleteTestAsync(int id)
        {
           if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            return await _repo.DeleteAsync(id);
        }

        public async Task<TestDTO?> FindByIDAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }
            var test = await _repo.FindAsync(id);
            return test is null ? null : _mapper.Map<TestDTO>(test);
        }

    }
}
