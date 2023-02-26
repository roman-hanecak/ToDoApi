using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ToDoApi.Database;
using ToDoApi.Entities.Domain;
using ToDoApi.Entities.DTO;
using ToDoApi.Exceptions;
using ToDoApi.Services;
using ToDoApi.Services.Interfaces;
using ToDoApi.Tests.Database;

namespace ToDoApi.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private TestApplicationContext _testContext;
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            //_userService =  new UserService(_context);//Substitute.For<IUserService>();
            _userService = Substitute.For<IUserService>();
            //_context = Substitute.For<TestApplicationContext>();
        }

        [Test]
        public async Task GetUserAsync_Returns_UserDto()
        {
            Guid userId = new Guid("e48a255e-9b73-48ba-879c-b3f947e0fd51");
            // UserDto userDto = new UserDto
            // {
            //     PublicId = userId,
            //     FirstName = "string",
            //     LastName = "string",
            //     Email = "ww@ww.com",
            //     Image = "string"
            // };


            //_userService.GetUserAsync(userId, Arg.Any<CancellationToken>()).ReturnsNull();
            //act
            UserDto user = await _userService.GetUserAsync(userId);
            //act && assert
            //
            Assert.IsNotNull(user);
            //Assert.AreEqual(user, userDto);
            //Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUserAsync(userId));
            //Assert.That(ex.Message == $"User with Id {userId} does not exist");
            //Assert.Pass();
        }
        [Test]
        public async Task GetUserAsync_InvalidUserId_ThrowsNotFoundException()
        {
            var userId = new Guid();

            // _userService
            //     .GetUserAsync(userId, Arg.Any<CancellationToken>())
            //     .Returns(Task.FromException(new NotFoundException("asdasd")));

            //_userService.GetUserAsync(userId, Arg.Any<CancellationToken>()).ReturnsNull();
            //act && assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUserAsync(userId));
            //Assert.That(async () => await _userService.GetUserAsync(userId, Arg.Any<CancellationToken>()), Throws.TypeOf<NotFoundException>());
            //Assert.That(ex.Message == $"User with Id {userId} does not exist");
            //Assert.Pass();
        }
    }
}