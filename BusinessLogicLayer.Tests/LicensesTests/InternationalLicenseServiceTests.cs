using AutoMapper;
using BusinessLoginLayer.Services.Licenses;
using Core.DTOs.License;
using Core.Interfaces.Repositories.Common;
using Core.Interfaces.Repositories.Licenses;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Licenses;
using DataAccessLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicLayer.Tests.LicensesTests
{
    public class InternationalLicenseServiceTests
    {
        private readonly Mock<IRepository<InternationalLicense>> _repoMock;
        private readonly Mock<ILicenseService> _licenseServiceMock;
        private readonly Mock<IApplicationService> _applicationServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly InternationalLicenseService _service;

        public InternationalLicenseServiceTests()
        {
            _repoMock = new Mock<IRepository<InternationalLicense>>();
            _licenseServiceMock = new Mock<ILicenseService>();
            _applicationServiceMock = new Mock<IApplicationService>();
            _mapperMock = new Mock<IMapper>();
            _service = new InternationalLicenseService(
                _mapperMock.Object,
                _repoMock.Object,
                _licenseServiceMock.Object,
                _applicationServiceMock.Object
            );
        }

        #region IssueInternationalLicense Tests

        [Fact]
        public async Task IssueInternationalLicense_NullDto_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.IssueInternationalLicense(null));
        }

        [Fact]
        public async Task IssueInternationalLicense_InvalidLocalLicenseId_ThrowsArgumentOutOfRangeException()
        {
            var dto = new InternationalLicenseDTO { IssuedUsingLocalLicenseID = 0 };
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.IssueInternationalLicense(dto));
        }

        [Fact]
        public async Task IssueInternationalLicense_LocalLicenseNotActive_ThrowsInvalidOperationException()
        {
            var dto = new InternationalLicenseDTO { IssuedUsingLocalLicenseID = 1 };
            _licenseServiceMock.Setup(s => s.IsLicenseActive(dto.IssuedUsingLocalLicenseID)).ReturnsAsync(false);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.IssueInternationalLicense(dto));
        }

        [Fact]
        public async Task IssueInternationalLicense_LicenseExpired_ThrowsInvalidOperationException()
        {
            var dto = new InternationalLicenseDTO { IssuedUsingLocalLicenseID = 1 };
            _licenseServiceMock.Setup(s => s.IsLicenseActive(dto.IssuedUsingLocalLicenseID)).ReturnsAsync(true);
            _licenseServiceMock.Setup(s => s.IsLicenseExpired(dto.IssuedUsingLocalLicenseID)).ReturnsAsync(true);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.IssueInternationalLicense(dto));
        }

        [Fact]
        public async Task IssueInternationalLicense_AlreadyHasActiveInternationalLicense_ThrowsInvalidOperationException()
        {
            var dto = new InternationalLicenseDTO { IssuedUsingLocalLicenseID = 1 };
            _licenseServiceMock.Setup(s => s.IsLicenseActive(dto.IssuedUsingLocalLicenseID)).ReturnsAsync(true);
            _licenseServiceMock.Setup(s => s.IsLicenseExpired(dto.IssuedUsingLocalLicenseID)).ReturnsAsync(false);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.IssueInternationalLicense(dto));
        }

        [Fact]
        public async Task IssueInternationalLicense_ValidInput_ReturnsNewId()
        {
            // Arrange
            var dto = new InternationalLicenseDTO { IssuedUsingLocalLicenseID = 1 };
            var entity = new InternationalLicense();
            var license = new License { DriverID = 5 };

            _licenseServiceMock.Setup(s => s.IsLicenseActive(dto.IssuedUsingLocalLicenseID)).ReturnsAsync(true);
            _licenseServiceMock.Setup(s => s.IsLicenseExpired(dto.IssuedUsingLocalLicenseID)).ReturnsAsync(false);
            _licenseServiceMock.Setup(s => s.FindByIDAsync(dto.IssuedUsingLocalLicenseID)).ReturnsAsync(new ReadLicenseDTO { DriverID = 5 });
            _mapperMock.Setup(m => m.Map<InternationalLicense>(dto)).Returns(entity);
            _repoMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(10);

            // Act
            var result = await _service.IssueInternationalLicense(dto);

            // Assert
            Assert.Equal(10, result);
        }

        #endregion

        #region DeleteInternationalLicenseAsync Tests

        [Fact]
        public async Task DeleteInternationalLicenseAsync_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.DeleteInternationalLicenseAsync(0));
        }

        [Fact]
        public async Task DeleteInternationalLicenseAsync_ValidId_ReturnsDeleteResult()
        {
            // Arrange
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteInternationalLicenseAsync(1);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region FindByIDAsync Tests

        [Fact]
        public async Task FindByIDAsync_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.FindByIDAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_NotFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((InternationalLicense)null);

            // Act
            var result = await _service.FindByIDAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task FindByIDAsync_Found_ReturnsMappedDto()
        {
            // Arrange
            var entity = new InternationalLicense();
            var dto = new ReadInternationalLicenseDTO();

            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<ReadInternationalLicenseDTO>(entity)).Returns(dto);

            // Act
            var result = await _service.FindByIDAsync(1);

            // Assert
            Assert.Equal(dto, result);
        }

        #endregion

        #region GetAllAsync Tests

        [Fact]
        public async Task GetAllAsync_NoLicenses_ReturnsEmptyList()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<InternationalLicense>());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_HasLicenses_ReturnsMappedDtos()
        {
            // Arrange
            var entities = new List<InternationalLicense> { new InternationalLicense() };
            var dtos = new List<ReadInternationalLicenseDTO> { new ReadInternationalLicenseDTO() };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<ReadInternationalLicenseDTO>>(entities)).Returns(dtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(dtos, result);
        }

        #endregion
    }
}