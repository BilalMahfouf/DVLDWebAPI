using Core.Common;
using Core.DTOs.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Tests
{
    public interface ITestService
    {
        Task<TestDTO?> FindByIDAsync(int id);
        Task<int> CreateTestAsync(TestDTO testDTO);
        Task<bool> DeleteTestAsync(int id);
        Task<int> GetTestFailedTrails(int id, Enums.LicenseClassTypeEnum
            licenseClassType);
        Task<int> GetPassedTests(int id, Enums.LicenseClassTypeEnum
            licenseClassType);

    }
}
