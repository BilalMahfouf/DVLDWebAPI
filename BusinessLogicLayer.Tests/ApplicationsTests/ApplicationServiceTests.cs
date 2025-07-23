using AutoMapper;
using BusinessLoginLayer.Services.Applications;
using Core.DTOs.Application;
using Core.Interfaces.Repositories.Applications;
using DataAccessLayer;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using static Core.Common.Enums;

namespace BusinessLogicLayer.Tests.ApplicationsTests
{
    public class ApplicationServiceTests
    {
        private readonly Mock<IApplicationRepository> _applicationRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicationService _applicationService;

        public ApplicationServiceTests()
        {
            _applicationRepositoryMock = new Mock<IApplicationRepository>();
            _mapperMock = new Mock<IMapper>();
            _applicationService = new ApplicationService(_applicationRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CancelApplication_WithValidId_UpdatesStatusToCanceled()
        {
            // Arrange
            var application = new Application { ApplicationID = 1, ApplicationStatus = (byte)ApplicationStatusEnum.New };
            _applicationRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(application);
            _applicationRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Application>())).ReturnsAsync(true);

            // Act
            var result = await _applicationService.CancelApplication(1);

            // Assert
            Assert.True(result);
            Assert.Equal((byte)ApplicationStatusEnum.Canceled, application.ApplicationStatus);
            _applicationRepositoryMock.Verify(r => r.UpdateAsync(application), Times.Once);
        }

        [Fact]
        public async Task CompleteApplicationAsync_WithValidId_UpdatesStatusToCompleted()
        {
            // Arrange
            var application = new Application { ApplicationID = 1, ApplicationStatus = (byte)ApplicationStatusEnum.New };
            _applicationRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(application);
            _applicationRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Application>())).ReturnsAsync(true);

            // Act
            var result = await _applicationService.CompleteApplicationAsync(1);

            // Assert
            Assert.True(result);
            Assert.Equal((byte)ApplicationStatusEnum.Completed, application.ApplicationStatus);
            _applicationRepositoryMock.Verify(r => r.UpdateAsync(application), Times.Once);
        }

        [Fact]
        public async Task CreateApplicationAsync_WithValidDto_CreatesApplicationAndReturnsId()
        {
            // Arrange
            var applicationDto = new ApplicationDTO();
            var application = new Application();
            _mapperMock.Setup(m => m.Map<Application>(applicationDto)).Returns(application);
            _applicationRepositoryMock.Setup(r => r.AddAsync(application)).ReturnsAsync(10);

            // Act
            var result = await _applicationService.CreateApplicationAsync(applicationDto);

            // Assert
            Assert.Equal(10, result);
            Assert.Equal((byte)ApplicationTypeEnum.NewLocalDrivingLicense, application.ApplicationTypeID);
            Assert.Equal((byte)ApplicationStatusEnum.New, application.ApplicationStatus);
        }

        [Fact]
        public async Task CreateApplicationAsync_WithNullDto_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _applicationService.CreateApplicationAsync(null));
        }

        [Fact]
        public async Task DeleteApplicationAsync_WithValidId_DeletesAndReturnsTrue()
        {
            // Arrange
            _applicationRepositoryMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _applicationService.DeleteApplicationAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteApplicationAsync_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _applicationService.DeleteApplicationAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_WithValidId_ReturnsDto()
        {
            // Arrange
            var application = new Application();
            var readDto = new ReadApplicationDTO();
            _applicationRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(application);
            _mapperMock.Setup(m => m.Map<ReadApplicationDTO>(application)).Returns(readDto);

            // Act
            var result = await _applicationService.FindByIDAsync(1);

            // Assert
            Assert.Equal(readDto, result);
        }

        [Fact]
        public async Task FindByIDAsync_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _applicationService.FindByIDAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_WithNonExistentId_ReturnsNull()
        {
            // Arrange
            _applicationRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync((Application)null);

            // Act
            var result = await _applicationService.FindByIDAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task IsExistAsync_WhenExists_ReturnsTrue()
        {
            // Arrange
            _applicationRepositoryMock.Setup(r => r.IsExistAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _applicationService.IsExistAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsExistAsync_WhenNotExists_ReturnsFalse()
        {
            // Arrange
            _applicationRepositoryMock.Setup(r => r.IsExistAsync(1)).ReturnsAsync(false);

            // Act
            var result = await _applicationService.IsExistAsync(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsExistAsync_WithInvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _applicationService.IsExistAsync(0));
        }
    }
}