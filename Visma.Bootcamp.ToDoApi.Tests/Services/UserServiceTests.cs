using NSubstitute;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories;
using NSubstitute.ReturnsExtensions;

namespace Visma.Bootcamp.ToDoApi.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;
        private IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository);
        }

        [Test]
        public async Task GetUserAsync_Returns_UserDto()
        {
            Guid userId = new Guid("14c450ba-82e3-4459-9030-21d93398adae");
            User user = new User
            {
                Id = 6,
                PublicId = new Guid("14c450ba-82e3-4459-9030-21d93398adae"),
                FirstName = "string",
                LastName = "string",
                Email = "ww@ww.com",
                Password = "ww",
                Image = "string",
            };

            _userRepository.Get(userId).Returns(user);

            var userDto = await _userService.GetUserAsync(userId);

            Assert.IsNotNull(userDto);
            Assert.IsInstanceOf(typeof(UserDto), userDto);
            Assert.AreEqual(userDto.Email, user.Email);
            Assert.AreEqual(userDto.PublicId, user.PublicId);
            Assert.AreEqual(userDto.FirstName, user.FirstName);
            Assert.AreEqual(userDto.LastName, user.LastName);
            Assert.AreEqual(userDto.Image, user.Image);
        }

        [Test]
        public async Task GetUserAsync_InvalidGuid_Throws_NotFoundException()
        {
            Guid userId = new Guid("14c450ba-82e3-4459-9030-21d93398adae");

            _userRepository.Get(userId).ReturnsNull();

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUserAsync(userId));
        }

        [Test]
        public async Task DeleteUserAsync_InvalidUserGuid_Throws_()
        {
            Guid userId = new Guid("14c450ba-82e3-4459-9030-21d93398adae");

            _userRepository.Get(userId).ReturnsNull();

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.DeleteUserAsync(userId));
        }

        [Test]
        public async Task DeleteUserAsync_Successful_deletion()
        {
            Guid userId = new Guid("14c450ba-82e3-4459-9030-21d93398adae");

            User user = new User
            {
                Id = 6,
                PublicId = new Guid("14c450ba-82e3-4459-9030-21d93398adae"),
                FirstName = "string",
                LastName = "string",
                Email = "ww@ww.com",
                Password = "ww",
                Image = "string",
            };

            _userRepository.Get(userId).Returns(user);

            Assert.DoesNotThrowAsync(async () => await _userService.DeleteUserAsync(userId));
        }
    }
}