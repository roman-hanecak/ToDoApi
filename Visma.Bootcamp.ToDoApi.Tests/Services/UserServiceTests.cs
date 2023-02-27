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
        public void GetUserAsync()
        {
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

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IQueryable<UserDto>>().Setup(m => m).Returns((IQueryable<UserDto>)user);
            // mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            // mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            // mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new UserService(mockContext.Object);
            var userDto = service.GetUserAsync(new Guid("14c450ba-82e3-4459-9030-21d93398adae"));

            Assert.AreEqual(user.Email, userDto.Email);
            //Assert.Pass();
        }


    }
}