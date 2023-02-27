using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Visma.Bootcamp.ToDoApi.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = Substitute.For<IUserService>();
        }

        [Test]
        public async Task GetUserAsync_Returns_UserDto()
        {
            Guid userId = new Guid("14c450ba-82e3-4459-9030-21d93398adae");
            UserDto user = new UserDto
            {
                //Id = 6,
                PublicId = new Guid("14c450ba-82e3-4459-9030-21d93398adae"),
                FirstName = "string",
                LastName = "string",
                Email = "ww@ww.com",
                //Password = "ww",
                Image = "string",
            };

            _userService.GetUserAsync(userId).Returns(user);

            var userDto = await _userService.GetUserAsync(userId);

            Assert.IsNotNull(userDto);
            Assert.IsInstanceOf(typeof(UserDto), userDto);
            Assert.AreSame(user, userDto);
        }

        [Test]
        public async Task GetUserAsync_InvalidUserMail_Throws_()
        {
            Guid userId = new Guid("14c450ba-82e3-4459-9030-21d93398adae");

            _userService.When(x => x.GetUserAsync(userId))
                .Do(x => { throw new NotFoundException($"User with Id {userId} does not exist"); });

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUserAsync(userId));

        }

        [Test]
        public async Task DeleteUserAsync_InvalidUserMail_Throws_()
        {
            Guid userId = new Guid("14c450ba-82e3-4459-9030-21d93398adae");

            _userService.When(x => x.DeleteUserAsync(userId))
                .Do(x => { throw new NotFoundException($"User with Id {userId} does not exist!"); });

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.DeleteUserAsync(userId));
        }

        [Test]
        public async Task DeleteUserAsync_Successful_deletion()
        {
            Guid userId = new Guid("14c450ba-82e3-4459-9030-21d93398adae");

            //_userService.When(x => x.DeleteUserAsync(userId)).Do( x => { return NoContent(); });

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.DeleteUserAsync(userId));
            //Assert.
        }



    }
}