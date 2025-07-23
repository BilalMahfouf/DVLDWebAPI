using AutoMapper;
using BusinessLoginLayer.Services.Licenses;
using Core.DTOs.License;
 
using Core.Interfaces.Repositories.Licenses;

using Core.Interfaces.Services.Applications;
using DataAccessLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Core.Common.Enums;

namespace BusinessLogicLayer.Tests.LicensesTests
{
    public class LicenseServiceTests
    {
        private readonly Mock<ILicenseRepository> _licenseRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IApplicationService> _applicationServiceMock;
        private readonly Mock<LicenseClassService> _licenseClassServiceMock;
        private readonly LicenseService _licenseService;

        public LicenseServiceTests()
        {
            _licenseRepositoryMock = new Mock<ILicenseRepository>();
            _mapperMock = new Mock<IMapper>();
            _applicationServiceMock = new Mock<IApplicationService>();
            // Mocking the concrete LicenseClassService. Its methods must be virtual.
            _licenseClassServiceMock = new Mock<LicenseClassService>(null, null);
            _licenseService = new LicenseService(
                _licenseRepositoryMock.Object,
                _mapperMock.Object,
                _applicationServiceMock.Object,
                _licenseClassServiceMock.Object
            );
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UpdateLicenseStatus_WithValidId_UpdatesStatus(bool isActive)
        {
            var license = new License { LicenseID = 1 };
            _licenseRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(license);
            _licenseRepositoryMock.Setup(r => r.UpdateAsync(license)).ReturnsAsync(true);

            var result = isActive
                ? await _licenseService.ActivateLicenseAsync(1)
                : await _licenseService.DeActivateLicenseAsync(1);

            Assert.True(result);
            Assert.Equal(isActive, license.IsActive);
        }

        [Fact]
        public async Task UpdateLicenseStatus_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _licenseService.ActivateLicenseAsync(0));
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _licenseService.DeActivateLicenseAsync(0));
        }

        [Fact]
        public async Task UpdateLicenseStatus_LicenseNotFound_ThrowsArgumentException()
        {
            _licenseRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync((License)null);
            await Assert.ThrowsAsync<ArgumentException>(() => _licenseService.ActivateLicenseAsync(1));
        }

        [Fact]
        public async Task DeleteLicenseAsync_WithValidId_DeletesAndReturnsTrue()
        {
            _licenseRepositoryMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
            var result = await _licenseService.DeleteLicenseAsync(1);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteLicenseAsync_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _licenseService.DeleteLicenseAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_WithValidId_ReturnsDto()
        {
            var license = new License();
            var dto = new ReadLicenseDTO();
            _licenseRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(license);
            _mapperMock.Setup(m => m.Map<ReadLicenseDTO>(license)).Returns(dto);

            var result = await _licenseService.FindByIDAsync(1);

            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task GetAllLicenseAsync_WhenLicensesExist_ReturnsDtos()
        {
            var licenses = new List<License> { new License() };
            var dtos = new List<ReadLicenseDTO> { new ReadLicenseDTO() };
            _licenseRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(licenses);
            _mapperMock.Setup(m => m.Map<IEnumerable<ReadLicenseDTO>>(licenses)).Returns(dtos);

            var result = await _licenseService.GetAllLicenseAsync();

            Assert.Equal(dtos, result);
        }

        [Fact]
        public async Task IsLicenseActive_ReturnsCorrectStatus()
        {
            _licenseRepositoryMock.Setup(r => r.IsLicenseActiveAsync(1)).ReturnsAsync(true);
            var result = await _licenseService.IsLicenseActive(1);
            Assert.True(result);
        }

        [Fact]
        public async Task IssueNewDrivingLicenseAsync_WithValidDto_IssuesLicense()
        {
            var dto = new LicenseDTO { ApplicationID = 1, LicenseClass = LicenseClassTypeEnum.OrdinaryDrivingLicense };
            var license = new License();
            _applicationServiceMock.Setup(s => s.IsExistAsync(1)).ReturnsAsync(true);
            _applicationServiceMock.Setup(s => s.CompleteApplicationAsync(1)).ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<License>(dto)).Returns(license);
            _licenseClassServiceMock.Setup(s => s.GetLicenseValidityLengthAsync((int)dto.LicenseClass)).ReturnsAsync((byte)5);
            _licenseRepositoryMock.Setup(r => r.AddAsync(license)).ReturnsAsync(100);

            var result = await _licenseService.IssueNewDrivingLicenseAsync(dto);

            Assert.Equal(100, result);
            Assert.Equal((byte)IssueReason.FirstTime, license.IssueReason);
        }

        [Fact]
        public async Task RenewLicenseAsync_WhenValid_RenewsAndReturnsId()
        {
            var dto = new LicenseDTO { ApplicationID = 1, LicenseClass = LicenseClassTypeEnum.OrdinaryDrivingLicense };
            var license = new License();
            _licenseRepositoryMock.Setup(r => r.IsExistAndActiveAsync(1)).ReturnsAsync(true);
            _licenseRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(new License { ExpirationDate = DateTime.UtcNow.AddDays(-1), IssueDate = DateTime.UtcNow.AddYears(-5) });
            _applicationServiceMock.Setup(s => s.IsExistAsync(1)).ReturnsAsync(true);
            _applicationServiceMock.Setup(s => s.CompleteApplicationAsync(1)).ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<License>(dto)).Returns(license);
            _licenseClassServiceMock.Setup(s => s.GetLicenseValidityLengthAsync((int)dto.LicenseClass)).ReturnsAsync((byte)5);
            _licenseRepositoryMock.Setup(r => r.AddAsync(license)).ReturnsAsync(101);

            var result = await _licenseService.RenewLicenseAsync(1, dto);

            Assert.Equal(101, result);
            Assert.Equal((byte)IssueReason.Renew, license.IssueReason);
        }

        [Fact]
        public async Task IssueReplacementForLostLicenseAsync_WhenValid_IssuesAndReturnsId()
        {
            var dto = new LicenseDTO { ApplicationID = 1, LicenseClass = LicenseClassTypeEnum.OrdinaryDrivingLicense };
            var license = new License();
            _licenseRepositoryMock.Setup(r => r.IsExistAndActiveAsync(1)).ReturnsAsync(true);
            _licenseRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(new License { ExpirationDate = DateTime.UtcNow.AddDays(10), IssueDate = DateTime.UtcNow.AddYears(-1) });
            _applicationServiceMock.Setup(s => s.IsExistAsync(1)).ReturnsAsync(true);
            _applicationServiceMock.Setup(s => s.CompleteApplicationAsync(1)).ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<License>(dto)).Returns(license);
            _licenseClassServiceMock.Setup(s => s.GetLicenseValidityLengthAsync((int)dto.LicenseClass)).ReturnsAsync((byte)5);
            _licenseRepositoryMock.Setup(r => r.AddAsync(license)).ReturnsAsync(102);

            var result = await _licenseService.IssueReplacementForLostLicenseAsync(1, dto);

            Assert.Equal(102, result);
            Assert.Equal((byte)IssueReason.ReplacementForLost, license.IssueReason);
        }

        [Fact]
        public async Task IssueReplacementForDamagedLicenseAsync_WhenValid_IssuesAndReturnsId()
        {
            var dto = new LicenseDTO { ApplicationID = 1, LicenseClass = LicenseClassTypeEnum.OrdinaryDrivingLicense };
            var license = new License();
            _licenseRepositoryMock.Setup(r => r.IsExistAndActiveAsync(1)).ReturnsAsync(true);
            _licenseRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(new License { ExpirationDate = DateTime.UtcNow.AddDays(10), IssueDate = DateTime.UtcNow.AddYears(-1) });
            _applicationServiceMock.Setup(s => s.IsExistAsync(1)).ReturnsAsync(true);
            _applicationServiceMock.Setup(s => s.CompleteApplicationAsync(1)).ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<License>(dto)).Returns(license);
            _licenseClassServiceMock.Setup(s => s.GetLicenseValidityLengthAsync((int)dto.LicenseClass)).ReturnsAsync((byte)5);
            _licenseRepositoryMock.Setup(r => r.AddAsync(license)).ReturnsAsync(103);

            var result = await _licenseService.IssueReplacementForDamagedLicenseAsync(1, dto);

            Assert.Equal(103, result);
            Assert.Equal((byte)IssueReason.ReplacementForDamaged, license.IssueReason);
        }
    }
}