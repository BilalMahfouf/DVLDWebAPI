using AutoMapper;
using BusinessLoginLayer.Services.Applications;
using Core.DTOs.Application;

using Core.Interfaces.Repositories.Applications;

using Core.Interfaces.Services.Applications;
using DataAccessLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Core.Common.Enums;

namespace BusinessLogicLayer.Tests.ApplicationsTests
{
    public class LocalDrivingLicenseApplicationServiceTests
    {
        private readonly Mock<ILocalDrivingLicenseApplicationRepository> _localDLAppRepositoryMock;
        private readonly Mock<IApplicationService> _applicationServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly LocalDrivingLicenseApplicationService _service;

        public LocalDrivingLicenseApplicationServiceTests()
        {
            _localDLAppRepositoryMock = new Mock<ILocalDrivingLicenseApplicationRepository>();
            _applicationServiceMock = new Mock<IApplicationService>();
            _mapperMock = new Mock<IMapper>();
            _service = new LocalDrivingLicenseApplicationService(
                _mapperMock.Object,
                _localDLAppRepositoryMock.Object,
                _applicationServiceMock.Object
            );
        }

        [Fact]
        public async Task CanCreateLDLApplication_PersonIdInvalid_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.CanCreateLDLApplication(0, LicenseClassTypeEnum.SmallMotorcycle));
        }

        [Fact]
        public async Task CanCreateLDLApplication_AppExists_ReturnsFalse()
        {
            _localDLAppRepositoryMock.Setup(r => r.IsExistNewAppAsync(1, (int)LicenseClassTypeEnum.SmallMotorcycle)).ReturnsAsync(true);
            var result = await _service.CanCreateLDLApplication(1, LicenseClassTypeEnum.SmallMotorcycle);
            Assert.False(result);
        }

        [Fact]
        public async Task CanCreateLDLApplication_AppDoesNotExist_ReturnsTrue()
        {
            _localDLAppRepositoryMock.Setup(r => r.IsExistNewAppAsync(1, (int)LicenseClassTypeEnum.SmallMotorcycle)).ReturnsAsync(false);
            var result = await _service.CanCreateLDLApplication(1, LicenseClassTypeEnum.SmallMotorcycle);
            Assert.True(result);
        }

        [Fact]
        public async Task CreateLDLApplicationAsync_DtoIsNull_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateLDLApplicationAsync(null));
        }

        [Fact]
        public async Task CreateLDLApplicationAsync_ApplicationDoesNotExist_ThrowsInvalidOperationException()
        {
            var ldlDto = new LocalDrivingLicenseDTO { ApplicationID = 1 };
            _applicationServiceMock.Setup(s => s.IsExistAsync(1)).ReturnsAsync(false);
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateLDLApplicationAsync(ldlDto));
        }

        [Fact]
        public async Task CreateLDLApplicationAsync_ValidDto_ReturnsInsertedId()
        {
            var ldlDto = new LocalDrivingLicenseDTO { ApplicationID = 1 };
            var ldlApp = new LocalDrivingLicenseApplication();
            _applicationServiceMock.Setup(s => s.IsExistAsync(1)).ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<LocalDrivingLicenseApplication>(ldlDto)).Returns(ldlApp);
            _localDLAppRepositoryMock.Setup(r => r.AddAsync(ldlApp)).ReturnsAsync(100);

            var result = await _service.CreateLDLApplicationAsync(ldlDto);

            Assert.Equal(100, result);
        }

        [Fact]
        public async Task DeleteLDLApplicationAsync_IdIsInvalid_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.DeleteLDLApplicationAsync(0));
        }

        [Fact]
        public async Task DeleteLDLApplicationAsync_DeleteFails_ReturnsFalse()
        {
            _localDLAppRepositoryMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(false);
            var result = await _service.DeleteLDLApplicationAsync(1);
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteLDLApplicationAsync_AppNotFoundAfterDelete_ThrowsInvalidOperationException()
        {
            _localDLAppRepositoryMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
            _localDLAppRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync((LocalDrivingLicenseApplication)null);
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.DeleteLDLApplicationAsync(1));
        }

        [Fact]
        public async Task DeleteLDLApplicationAsync_Successful_ReturnsTrue()
        {
            var ldlApp = new LocalDrivingLicenseApplication { ApplicationID = 5 };
            _localDLAppRepositoryMock.Setup(r => r.DeleteAsync(5)).ReturnsAsync(true);
            _localDLAppRepositoryMock.Setup(r => r.FindAsync(5)).ReturnsAsync(ldlApp);
            _applicationServiceMock.Setup(s => s.DeleteApplicationAsync(ldlApp.ApplicationID)).ReturnsAsync(true);

            var result = await _service.DeleteLDLApplicationAsync(5);

            Assert.True(result);
        }

        [Fact]
        public async Task FindLDLAppByIDAsync_IdIsInvalid_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.FindLDLAppByIDAsync(0));
        }

        [Fact]
        public async Task FindLDLAppByIDAsync_AppNotFound_ReturnsNull()
        {
            _localDLAppRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync((LocalDrivingLicenseApplication)null);
            var result = await _service.FindLDLAppByIDAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task FindLDLAppByIDAsync_AppFound_ReturnsDto()
        {
            var ldlApp = new LocalDrivingLicenseApplication();
            var ldlDto = new LocalDrivingLicenseDTO();
            _localDLAppRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(ldlApp);
            _mapperMock.Setup(m => m.Map<LocalDrivingLicenseDTO>(ldlApp)).Returns(ldlDto);

            var result = await _service.FindLDLAppByIDAsync(1);

            Assert.Equal(ldlDto, result);
        }

        [Fact]
        public async Task GetAllAsync_NoApps_ReturnsEmpty()
        {

            _localDLAppRepositoryMock.Setup(r => r.GetAll_ViewAsync()).ReturnsAsync(new List<LocalDrivingLicenseApplications_View>());
            _mapperMock.Setup(m => m.Map<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>>(It.IsAny<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>>()))
                .Returns(new List<LocalDrivingLicenseApplicationDashboardDTO>());

            var result = await _service.GetAllAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_AppsExist_ReturnsDtos()
        {
            var apps = new List<LocalDrivingLicenseApplications_View> { new LocalDrivingLicenseApplications_View() };
            var dtos = new List<LocalDrivingLicenseApplicationDashboardDTO> { new LocalDrivingLicenseApplicationDashboardDTO() };
            _localDLAppRepositoryMock.Setup(r => r.GetAll_ViewAsync()).ReturnsAsync(apps);
            _mapperMock.Setup(m => m.Map<IEnumerable<LocalDrivingLicenseApplicationDashboardDTO>>(apps)).Returns(dtos);

            var result = await _service.GetAllAsync();

            Assert.Equal(dtos, result);
        }
    }
}