using AutoMapper;
using BusinessLoginLayer.Services.Licenses;
using Core.DTOs.License;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicLayer.Tests.LicensesTests
{
    public class LicenseClassServiceTests
    {
        private readonly Mock<IReadUpdateRepository<LicenseClass>> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly LicenseClassService _service;

        public LicenseClassServiceTests()
        {
            _repoMock = new Mock<IReadUpdateRepository<LicenseClass>>();
            _mapperMock = new Mock<IMapper>();
            _service = new LicenseClassService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenClassesExist_ReturnsDtos()
        {
            // Arrange
            var licenseClasses = new List<LicenseClass> { new LicenseClass() };
            var dtos = new List<LicenseClassDTO> { new LicenseClassDTO() };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(licenseClasses);
            _mapperMock.Setup(m => m.Map<IEnumerable<LicenseClassDTO>>(licenseClasses)).Returns(dtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(dtos, result);
        }

        [Fact]
        public async Task FindByIDAsync_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.FindByIDAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_WhenNotFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((LicenseClass)null);

            // Act
            var result = await _service.FindByIDAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task FindByIDAsync_WhenFound_ReturnsDto()
        {
            // Arrange
            var licenseClass = new LicenseClass();
            var dto = new LicenseClassDTO();
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(licenseClass);
            _mapperMock.Setup(m => m.Map<LicenseClassDTO>(licenseClass)).Returns(dto);

            // Act
            var result = await _service.FindByIDAsync(1);

            // Assert
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task UpdateFeesAsync_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.UpdateFeesAsync(0, 100));
        }

        [Fact]
        public async Task UpdateFeesAsync_WithNegativeFees_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.UpdateFeesAsync(1, -100));
        }

        [Fact]
        public async Task UpdateFeesAsync_WhenNotFound_ThrowsArgumentNullException()
        {
            // Arrange
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((LicenseClass)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateFeesAsync(1, 100));
        }

        [Fact]
        public async Task UpdateFeesAsync_WhenValid_UpdatesAndReturnsTrue()
        {
            // Arrange
            var licenseClass = new LicenseClass { ClassFees = 50 };
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(licenseClass);
            _repoMock.Setup(r => r.UpdateAsync(licenseClass)).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateFeesAsync(1, 100);

            // Assert
            Assert.True(result);
            Assert.Equal(100, licenseClass.ClassFees);
        }

        [Fact]
        public async Task GetLicenseValidityLengthAsync_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.GetLicenseValidityLengthAsync(0));
        }

        [Fact]
        public async Task GetLicenseValidityLengthAsync_WhenNotFound_ThrowsArgumentNullException()
        {
            // Arrange
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((LicenseClass)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetLicenseValidityLengthAsync(1));
        }

        [Fact]
        public async Task GetLicenseValidityLengthAsync_WhenFound_ReturnsLength()
        {
            // Arrange
            var licenseClass = new LicenseClass { DefaultValidityLength = 10 };
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(licenseClass);

            // Act
            var result = await _service.GetLicenseValidityLengthAsync(1);

            // Assert
            Assert.Equal(10, result);
        }
    }
}