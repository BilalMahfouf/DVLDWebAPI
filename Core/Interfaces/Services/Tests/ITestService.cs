using Core.Common;
using Core.DTOs.Test;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Tests
{
    public interface ITestService
    {
        Task<GenericResult<TestDTO>> FindByIDAsync(int id);
        Task<GenericResult<int>> CreateTestAsync(TestDTO testDTO);
        Task<Result> DeleteTestAsync(int id);
       
    }
}
