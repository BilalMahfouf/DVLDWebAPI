using AutoMapper;
using BusinessLoginLayer.Services.Tests;
using Core.DTOs.Test;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicLayer.Tests.TestsTests
{
    public class TestAppointmentServiceTests
    {
        private readonly Mock<IRepository<TestAppointment>> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TestAppointmentService _service;

        public TestAppointmentServiceTests()
        {
            _repoMock = new Mock<IRepository<TestAppointment>>();
            _mapperMock = new Mock<IMapper>();
            _service = new TestAppointmentService(_mapperMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task CreateTestAppointmentAsync_NullDto_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateTestAppointmentAsync(null));
        }

        [Fact]
        public async Task CreateTestAppointmentAsync_ValidDto_ReturnsNewId()
        {
            var dto = new TestAppointmentDTO();
            var appointment = new TestAppointment();
            _mapperMock.Setup(m => m.Map<TestAppointment>(dto)).Returns(appointment);
            _repoMock.Setup(r => r.AddAsync(appointment)).ReturnsAsync(1);

            var result = await _service.CreateTestAppointmentAsync(dto);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteTestAppointmentAsync_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.DeleteTestAppointmentAsync(0));
        }

        [Fact]
        public async Task DeleteTestAppointmentAsync_ValidId_ReturnsTrueOnSuccess()
        {
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
            var result = await _service.DeleteTestAppointmentAsync(1);
            Assert.True(result);
        }

        [Fact]
        public async Task FindByIDAsync_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.FindByIDAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_NotFound_ReturnsNull()
        {
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((TestAppointment)null);
            var result = await _service.FindByIDAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task FindByIDAsync_Found_ReturnsDto()
        {
            var appointment = new TestAppointment();
            var dto = new TestAppointmentDTO();
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(appointment);
            _mapperMock.Setup(m => m.Map<TestAppointmentDTO>(appointment)).Returns(dto);

            var result = await _service.FindByIDAsync(1);

            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task GetAllTestAppointmentAsync_NoneFound_ReturnsEmpty()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TestAppointment>());
            var result = await _service.GetAllTestAppointmentAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllTestAppointmentAsync_Found_ReturnsDtos()
        {
            var appointments = new List<TestAppointment> { new TestAppointment() };
            var dtos = new List<TestAppointmentDTO> { new TestAppointmentDTO() };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(appointments);
            _mapperMock.Setup(m => m.Map<IEnumerable<TestAppointmentDTO>>(appointments)).Returns(dtos);

            var result = await _service.GetAllTestAppointmentAsync();

            Assert.Equal(dtos, result);
        }

        [Fact]
        public async Task LockTestAppointment_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.LockTestAppointment(0));
        }

        [Fact]
        public async Task LockTestAppointment_NotFound_ThrowsArgumentNullException()
        {
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((TestAppointment)null);
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.LockTestAppointment(1));
        }

        [Fact]
        public async Task LockTestAppointment_Valid_LocksAndReturnsTrue()
        {
            var appointment = new TestAppointment { IsLocked = false };
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(appointment);
            _repoMock.Setup(r => r.UpdateAsync(appointment)).ReturnsAsync(true);

            var result = await _service.LockTestAppointment(1);

            Assert.True(result);
            Assert.True(appointment.IsLocked);
        }

        [Fact]
        public async Task UpdateTestAppointmentAsync_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.UpdateTestAppointmentAsync(0, 1));
        }

        [Fact]
        public async Task UpdateTestAppointmentAsync_NotFound_ThrowsArgumentNullException()
        {
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((TestAppointment)null);
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateTestAppointmentAsync(1, 1));
        }

        [Fact]
        public async Task UpdateTestAppointmentAsync_Valid_UpdatesAndReturnsTrue()
        {
            var appointment = new TestAppointment();
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(appointment);
            _repoMock.Setup(r => r.UpdateAsync(appointment)).ReturnsAsync(true);

            var result = await _service.UpdateTestAppointmentAsync(1, 5);

            Assert.True(result);
            Assert.Equal(5, appointment.RetakeTestApplicationID);
        }
    }
}