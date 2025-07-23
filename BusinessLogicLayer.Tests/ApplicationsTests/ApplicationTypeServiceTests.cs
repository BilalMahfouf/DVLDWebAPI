using AutoMapper;
using BusinessLoginLayer.Services.Applications;
using Core.DTOs.Application;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicLayer.Tests.ApplicationsTests
{
    public class ApplicationTypeServiceTests
    {
        private readonly Mock<IReadUpdateRepository<ApplicationType>> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicationTypeService _applicationTypeService;

        public ApplicationTypeServiceTests()
        {
            _repoMock = new Mock<IReadUpdateRepository<ApplicationType>>();
            _mapperMock = new Mock<IMapper>();
            _applicationTypeService = new ApplicationTypeService(_mapperMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task GetAllApplicationTypesAsync_WhenTypesExist_ReturnsDtos()
        {
            // Arrange
            var appTypes = new List<ApplicationType> { new ApplicationType(), new ApplicationType() };
            var appTypeDtos = new List<ReadApplicationDTO> { new ReadApplicationDTO(), new ReadApplicationDTO() };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(appTypes);
            _mapperMock.Setup(m => m.Map<IEnumerable<ReadApplicationDTO>>(appTypes)).Returns(appTypeDtos);

            // Act
            var result = await _applicationTypeService.GetAllApplicationTypesAsync();

            // Assert
            Assert.Equal(appTypeDtos, result);
        }

        [Fact]
        public async Task GetAllApplicationTypesAsync_WhenNoTypesExist_ReturnsEmptyList()
        {
            // Arrange
            var appTypes = new List<ApplicationType>();
            var appTypeDtos = new List<ReadApplicationDTO>();
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(appTypes);
            _mapperMock.Setup(m => m.Map<IEnumerable<ReadApplicationDTO>>(appTypes)).Returns(appTypeDtos);

            // Act
            var result = await _applicationTypeService.GetAllApplicationTypesAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateFeesAsync_WithValidData_UpdatesFeesAndReturnsTrue()
        {
            // Arrange
            var appType = new ApplicationType { ApplicationTypeID = 1, ApplicationFees = 50 };
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(appType);
            _repoMock.Setup(r => r.UpdateAsync(appType)).ReturnsAsync(true);

            // Act
            var result = await _applicationTypeService.UpdateFeesAsync(1, 100);

            // Assert
            Assert.True(result);
            Assert.Equal(100, appType.ApplicationFees);
            _repoMock.Verify(r => r.UpdateAsync(appType), Times.Once);
        }

        [Fact]
        public async Task UpdateFeesAsync_WithInvalidId_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _applicationTypeService.UpdateFeesAsync(0, 100));
        }

        [Fact]
        public async Task UpdateFeesAsync_WithNegativeFees_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _applicationTypeService.UpdateFeesAsync(1, -50));
        }

        [Fact]
        public async Task UpdateFeesAsync_WithNonExistentId_ThrowsArgumentNullException()
        {
            // Arrange
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((ApplicationType)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _applicationTypeService.UpdateFeesAsync(1, 100));
        }

        [Fact]
        public async Task FindByIDAsync_WithValidId_ReturnsDto()
        {
            // Arrange
            var appType = new ApplicationType();
            var appTypeDto = new ReadApplicationDTO();
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(appType);
            _mapperMock.Setup(m => m.Map<ReadApplicationDTO>(appType)).Returns(appTypeDto);

            // Act
            var result = await _applicationTypeService.FindByIDAsync(1);

            // Assert
            Assert.Equal(appTypeDto, result);
        }

        [Fact]
        public async Task FindByIDAsync_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _applicationTypeService.FindByIDAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_WithNonExistentId_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((ApplicationType)null);

            // Act
            var result = await _applicationTypeService.FindByIDAsync(1);

            // Assert
            Assert.Null(result);
        }
    }
}