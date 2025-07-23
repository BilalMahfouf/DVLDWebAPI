using AutoMapper;
using BusinessLoginLayer.Services.Licenses;
using Core.DTOs.Detain;
using Core.Interfaces.Repositories.Common;
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
    public class DetainedLicenseServiceTests
    {
        private readonly Mock<IRepository<DetainedLicense>> _detainedLicenseRepoMock;
        private readonly Mock<IApplicationService> _applicationServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly DetainLicenseService _service;
        private readonly Mock<ILicenseService> _licenseServiceMock;

        public DetainedLicenseServiceTests()
        {
            _detainedLicenseRepoMock = new Mock<IRepository<DetainedLicense>>();
            _applicationServiceMock = new Mock<IApplicationService>();
            _mapperMock = new Mock<IMapper>();
            _licenseServiceMock = new Mock<ILicenseService>();
            _service = new DetainLicenseService(
               _detainedLicenseRepoMock.Object,
               _applicationServiceMock.Object,
               _mapperMock.Object,_licenseServiceMock.Object

           );
        }

        #region CreateDetainedLicenseAsync Tests

        [Fact]
        public async Task CreateDetainedLicenseAsync_WithNullDto_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateDetainedLicenseAsync(null));
        }

        
       
        [Fact]
        public async Task CreateDetainedLicenseAsync_ValidData_ReturnsId()
        {
            // Arrange
            var dto = new DetainLicenseDTO
            {
                LicenseID = 1,
                FineFees = 100,
                CreatedByUserID = 5
            };
            var entity = new DetainedLicense();
            _mapperMock.Setup(m => m.Map<DetainedLicense>(dto)).Returns(entity);
            _licenseServiceMock.Setup(s => s.IsLicenseExistAndActiveAsync(dto.LicenseID)).ReturnsAsync(true);
            _detainedLicenseRepoMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(10);

            // Act
            var result = await _service.CreateDetainedLicenseAsync(dto);

            // Assert
            Assert.Equal(10, result);
            Assert.False(entity.IsReleased);
        }

        #endregion

        #region DeleteDetainedLicenseAsync Tests

        [Fact]
        public async Task DeleteDetainedLicenseAsync_WithInvalidId_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.DeleteDetainedLicenseAsync(0));
        }

        [Fact]
        public async Task DeleteDetainedLicenseAsync_ValidId_ReturnsRepositoryResult()
        {
            // Arrange
            _detainedLicenseRepoMock.Setup(r => r.DeleteAsync(5)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteDetainedLicenseAsync(5);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region FindAsync Tests

        [Fact]
        public async Task FindAsync_WithInvalidId_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.FindAsync(0));
        }

        [Fact]
        public async Task FindAsync_NotFound_ReturnsNull()
        {
            // Arrange
            _detainedLicenseRepoMock.Setup(r => r.FindAsync(5)).ReturnsAsync((DetainedLicense)null);

            // Act
            var result = await _service.FindAsync(5);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task FindAsync_Found_ReturnsMappedDto()
        {
            // Arrange
            var entity = new DetainedLicense();
            var dto = new DetainLicenseDTO();
            _detainedLicenseRepoMock.Setup(r => r.FindAsync(5)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<DetainLicenseDTO>(entity)).Returns(dto);

            // Act
            var result = await _service.FindAsync(5);

            // Assert
            Assert.Equal(dto, result);
        }

        #endregion

        #region GetAllAsync Tests

        [Fact]
        public async Task GetAllAsync_NoRecords_ReturnsEmptyList()
        {
            // Arrange
            _detainedLicenseRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<DetainedLicense>());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_WithRecords_ReturnsMappedDtos()
        {
            // Arrange
            var entities = new List<DetainedLicense> { new DetainedLicense() };
            var dtos = new List<DetainLicenseDTO> { new DetainLicenseDTO() };
            _detainedLicenseRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<DetainLicenseDTO>>(entities)).Returns(dtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(dtos, result);
        }

        #endregion

        #region ReleaseLicenseAsync Tests

        [Fact]
        public async Task ReleaseLicenseAsync_WithNullDto_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.ReleaseLicenseAsync(null));
        }

        [Fact]
        public async Task ReleaseLicenseAsync_WithInvalidDetainId_ThrowsArgumentException()
        {
            var dto = new UpdateDetainedLicenseDTO { DetainID = 0 };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ReleaseLicenseAsync(dto));
        }

        [Fact]
        public async Task ReleaseLicenseAsync_WithInvalidUserOrAppId_ThrowsArgumentException()
        {
            var dto = new UpdateDetainedLicenseDTO
            {
                DetainID = 5,
                ReleasedByUserID = 0,
                ReleaseApplicationID = 0
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ReleaseLicenseAsync(dto));
        }

        [Fact]
        public async Task ReleaseLicenseAsync_DetainedLicenseNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            var dto = new UpdateDetainedLicenseDTO
            {
                DetainID = 5,
                ReleasedByUserID = 1,
                ReleaseApplicationID = 1
            };
            _detainedLicenseRepoMock.Setup(r => r.FindAsync(dto.DetainID)).ReturnsAsync((DetainedLicense)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ReleaseLicenseAsync(dto));
        }

        [Fact]
        public async Task ReleaseLicenseAsync_AlreadyReleased_ThrowsInvalidOperationException()
        {
            // Arrange
            var dto = new UpdateDetainedLicenseDTO
            {
                DetainID = 5,
                ReleasedByUserID = 1,
                ReleaseApplicationID = 1
            };
            var entity = new DetainedLicense { IsReleased = true };
            _detainedLicenseRepoMock.Setup(r => r.FindAsync(dto.DetainID)).ReturnsAsync(entity);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ReleaseLicenseAsync(dto));
        }

        [Fact]
        public async Task ReleaseLicenseAsync_ApplicationNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            var dto = new UpdateDetainedLicenseDTO
            {
                DetainID = 5,
                ReleasedByUserID = 1,
                ReleaseApplicationID = 1
            };
            var entity = new DetainedLicense { IsReleased = false };
            _detainedLicenseRepoMock.Setup(r => r.FindAsync(dto.DetainID)).ReturnsAsync(entity);
            _applicationServiceMock.Setup(s => s.IsExistAsync(dto.ReleaseApplicationID)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ReleaseLicenseAsync(dto));
        }

        [Fact]
        public async Task ReleaseLicenseAsync_Valid_UpdatesAndReturnsTrue()
        {
            // Arrange
            var dto = new UpdateDetainedLicenseDTO
            {
                DetainID = 5,
                ReleasedByUserID = 1,
                ReleaseApplicationID = 1
            };
            var entity = new DetainedLicense { IsReleased = false };
            _detainedLicenseRepoMock.Setup(r => r.FindAsync(dto.DetainID)).ReturnsAsync(entity);
            _applicationServiceMock.Setup(s => s.IsExistAsync(dto.ReleaseApplicationID)).ReturnsAsync(true);
            _applicationServiceMock.Setup(s => s.CompleteApplicationAsync(dto.ReleaseApplicationID)).ReturnsAsync(true);
            _detainedLicenseRepoMock.Setup(r => r.UpdateAsync(entity)).ReturnsAsync(true);

            // Act
            var result = await _service.ReleaseLicenseAsync(dto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ReleaseLicenseAsync_UpdateFails_ReturnsFalse()
        {
            // Arrange
            var dto = new UpdateDetainedLicenseDTO
            {
                DetainID = 5,
                ReleasedByUserID = 1,
                ReleaseApplicationID = 1
            };
            var entity = new DetainedLicense { IsReleased = false };
            _detainedLicenseRepoMock.Setup(r => r.FindAsync(dto.DetainID)).ReturnsAsync(entity);
            _applicationServiceMock.Setup(s => s.IsExistAsync(dto.ReleaseApplicationID)).ReturnsAsync(true);
            _applicationServiceMock.Setup(s => s.CompleteApplicationAsync(dto.ReleaseApplicationID)).ReturnsAsync(true);
            _detainedLicenseRepoMock.Setup(r => r.UpdateAsync(entity)).ReturnsAsync(false);

            // Act
            var result = await _service.ReleaseLicenseAsync(dto);

            // Assert
            Assert.False(result);
        }

        #endregion
    }
}