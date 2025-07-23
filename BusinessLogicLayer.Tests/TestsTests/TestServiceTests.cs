using AutoMapper;
using BusinessLoginLayer.Services.Tests;
using Core.DTOs.Test;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicLayer.Tests.TestsTests
{
    public class TestServiceTests
    {
        private readonly Mock<IRepository<Test>> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TestService _service;

        public TestServiceTests()
        {
            _repoMock = new Mock<IRepository<Test>>();
            _mapperMock = new Mock<IMapper>();
            _service = new TestService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateTestAsync_NullDto_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateTestAsync(null));
        }

        [Fact]
        public async Task CreateTestAsync_ValidDto_ReturnsNewId()
        {
            var dto = new TestDTO();
            var test = new Test();
            _mapperMock.Setup(m => m.Map<Test>(dto)).Returns(test);
            _repoMock.Setup(r => r.AddAsync(test)).ReturnsAsync(1);

            var result = await _service.CreateTestAsync(dto);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteTestAsync_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.DeleteTestAsync(0));
        }

        [Fact]
        public async Task DeleteTestAsync_ValidId_ReturnsTrueOnSuccess()
        {
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
            var result = await _service.DeleteTestAsync(1);
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
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync((Test)null);
            var result = await _service.FindByIDAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task FindByIDAsync_Found_ReturnsDto()
        {
            var test = new Test();
            var dto = new TestDTO();
            _repoMock.Setup(r => r.FindAsync(1)).ReturnsAsync(test);
            _mapperMock.Setup(m => m.Map<TestDTO>(test)).Returns(dto);

            var result = await _service.FindByIDAsync(1);

            Assert.Equal(dto, result);
        }
    }
}