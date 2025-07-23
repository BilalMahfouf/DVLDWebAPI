using AutoMapper;
using BusinessLoginLayer.Services.Tests;
using Core.DTOs.Test;
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicLayer.Tests.TestsTests
{
    public class TestTypeServiceTests
    {
        private readonly Mock<IReadUpdateRepository<TestType>> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TestTypeService _testTypeService;

        public TestTypeServiceTests()
        {
            _repoMock = new Mock<IReadUpdateRepository<TestType>>();
            _mapperMock = new Mock<IMapper>();
            _testTypeService = new TestTypeService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTestTypes()
        {
            // Arrange
            var testTypes = new List<TestType> { new TestType(), new TestType() };
            var testTypeDTOs = new List<TestTypeDTO> { new TestTypeDTO(), new TestTypeDTO() };
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(testTypes);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<TestTypeDTO>>(testTypes)).Returns(testTypeDTOs);

            // Act
            var result = await _testTypeService.GetAllAsync();

            // Assert
            Assert.Equal(testTypeDTOs, result);
        }

        [Fact]
        public async Task FindByIDAsync_WithValidId_ShouldReturnTestType()
        {
            // Arrange
            var testType = new TestType();
            var testTypeDTO = new TestTypeDTO();
            _repoMock.Setup(repo => repo.FindAsync(1)).ReturnsAsync(testType);
            _mapperMock.Setup(mapper => mapper.Map<TestTypeDTO>(testType)).Returns(testTypeDTO);

            // Act
            var result = await _testTypeService.FindByIDAsync(1);

            // Assert
            Assert.Equal(testTypeDTO, result);
        }

        [Fact]
        public async Task FindByIDAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _testTypeService.FindByIDAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.FindAsync(1)).ReturnsAsync((TestType)null);

            // Act
            var result = await _testTypeService.FindByIDAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateFeesAsync_WithValidData_ShouldUpdateFees()
        {
            // Arrange
            var testType = new TestType();
            _repoMock.Setup(repo => repo.FindAsync(1)).ReturnsAsync(testType);
            _repoMock.Setup(repo => repo.UpdateAsync(testType)).ReturnsAsync(true);

            // Act
            var result = await _testTypeService.UpdateFeesAsync(1, 100);

            // Assert
            Assert.True(result);
            Assert.Equal(100, testType.TestTypeFees);
        }

        [Fact]
        public async Task UpdateFeesAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _testTypeService.UpdateFeesAsync(0, 100));
        }

        [Fact]
        public async Task UpdateFeesAsync_WithNegativeFees_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _testTypeService.UpdateFeesAsync(1, -100));
        }

        [Fact]
        public async Task UpdateFeesAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            _repoMock.Setup(repo => repo.FindAsync(1)).ReturnsAsync((TestType)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _testTypeService.UpdateFeesAsync(1, 100));
        }
    }
}