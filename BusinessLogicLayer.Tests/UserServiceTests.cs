using AutoMapper;
using BusinessLoginLayer.Services;
using Core.DTOs.User;
using Core.Interfaces.Repositories.Users;
using DataAccessLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicLayer.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_mapperMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task ActivateAsync_ValidId_ActivatesUser()
        {
            var user = new User { IsActive = false };
            _userRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.UpdateAsync(user)).ReturnsAsync(true);

            var result = await _userService.ActivateAsync(1);

            Assert.True(result);
            Assert.True(user.IsActive);
            _userRepositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task ActivateAsync_IdLessThanOrEqualZero_Throws()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _userService.ActivateAsync(0));
        }

        [Fact]
        public async Task ActivateAsync_UserNotFound_Throws()
        {
            _userRepositoryMock.Setup(r => r.FindAsync(2)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.ActivateAsync(2));
        }

        [Fact]
        public async Task CanCreateUserAsync_ValidPersonId_ReturnsTrueIfNotExist()
        {
            _userRepositoryMock.Setup(r => r.isExistByPersonID(1)).ReturnsAsync(false);

            var result = await _userService.CanCreateUserAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task CanCreateUserAsync_ValidPersonId_ReturnsFalseIfExist()
        {
            _userRepositoryMock.Setup(r => r.isExistByPersonID(1)).ReturnsAsync(true);

            var result = await _userService.CanCreateUserAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task CanCreateUserAsync_PersonIdLessThanOrEqualZero_Throws()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _userService.CanCreateUserAsync(0));
        }

        [Fact]
        public async Task CreateUserAsync_ValidDto_ReturnsInsertedId()
        {
            var dto = new CreateUserDTO();
            var user = new User();
            _mapperMock.Setup(m => m.Map<User>(dto)).Returns(user);
            _userRepositoryMock.Setup(r => r.AddAsync(user)).ReturnsAsync(5);

            var result = await _userService.CreateUserAsync(dto);

            Assert.Equal(5, result);
        }

        [Fact]
        public async Task CreateUserAsync_NullDto_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.CreateUserAsync(null));
        }

        [Fact]
        public async Task DeActivateAsync_ValidId_DeactivatesUser()
        {
            var user = new User { IsActive = true };
            _userRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.UpdateAsync(user)).ReturnsAsync(true);

            var result = await _userService.DeActivateAsync(1);

            Assert.True(result);
            Assert.False(user.IsActive);
            _userRepositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeActivateAsync_IdLessThanOrEqualZero_Throws()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _userService.DeActivateAsync(0));
        }

        [Fact]
        public async Task DeActivateAsync_UserNotFound_Throws()
        {
            _userRepositoryMock.Setup(r => r.FindAsync(2)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.DeActivateAsync(2));
        }

        [Fact]
        public async Task DeleteUserAsync_ValidId_DeletesUser()
        {
            _userRepositoryMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _userService.DeleteUserAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsync_IdLessThanOrEqualZero_Throws()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _userService.DeleteUserAsync(0));
        }

        [Fact]
        public async Task FindByIDAsync_ValidId_UserFound_ReturnsDto()
        {
            var user = new User();
            var dto = new ReadUserDTO();
            _userRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<ReadUserDTO>(user)).Returns(dto);

            var result = await _userService.FindByIDAsync(1);

            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task FindByIDAsync_ValidId_UserNotFound_ReturnsNull()
        {
            _userRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync((User)null);

            var result = await _userService.FindByIDAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task FindByIDAsync_IdLessThanOrEqualZero_Throws()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _userService.FindByIDAsync(0));
        }

        [Fact]
        public async Task GetAllAsync_UsersExist_ReturnsDtos()
        {
            var users = new List<User> { new User(), new User() };
            var dtos = new List<ReadUserDTO> { new ReadUserDTO(), new ReadUserDTO() };
            _userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);
            _mapperMock.Setup(m => m.Map<IEnumerable<ReadUserDTO>>(users)).Returns(dtos);

            var result = await _userService.GetAllAsync();

            Assert.Equal(dtos, result);
        }

        [Fact]
        public async Task GetAllAsync_NoUsers_ReturnsEmpty()
        {
            _userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ReadUserDTO>>(It.IsAny<IEnumerable<User>>()))
                .Returns(new List<ReadUserDTO>());

            var result = await _userService.GetAllAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateUserAsync_ValidInput_UpdatesUser()
        {
            var user = new User();
            var dto = new UpdateUserDTO();
            _userRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.UpdateAsync(user)).ReturnsAsync(true);

            var mapCall = _mapperMock.Setup(m => m.Map(dto, user));

            var result = await _userService.UpdateUserAsync(1, dto);

            Assert.True(result);
            mapCall.Verifiable();
            _userRepositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_IdLessThanOrEqualZero_Throws()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _userService.UpdateUserAsync(0, new UpdateUserDTO()));
        }

        [Fact]
        public async Task UpdateUserAsync_NullDto_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.UpdateUserAsync(1, null));
        }

        [Fact]
        public async Task UpdateUserAsync_UserNotFound_Throws()
        {
            _userRepositoryMock.Setup(r => r.FindAsync(1)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.UpdateUserAsync(1, new UpdateUserDTO()));
        }
    }
}