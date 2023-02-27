using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.ToDoApi.Tests.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        private IAuthService _authService;
        [SetUp]
        public void Setup()
        {
            _authService = Substitute.For<IAuthService>();
        }

        [Test]
        public async Task Login_happy_scenario()
        {
            string email = "ww@ww.com";
            string password = "ww";

            var result = new
            {
                Token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJ3d0B3dy5jb20iLCJ1bmlxdWVfbmFtZSI6Ind3QHd3LmNvbSIsIm5iZiI6MTY3NzQ0NDUzNiwiZXhwIjoxNjc3NTMwOTM2LCJpYXQiOjE2Nzc0NDQ1MzYsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTE0OS8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUxNDkvIn0.mZTl_Be6KOdhOY1XfBquPPC5KJy5WZpC5xBy2YK4tf45WSuasusqjqFusQDJr2yheOLJBsSwp251-EM_BLiXqg",
                User = new User
                {
                    Id = 6,
                    PublicId = new Guid("14c450ba-82e3-4459-9030-21d93398adae"),
                    FirstName = "string",
                    LastName = "string",
                    Email = "ww@ww.com",
                    Password = "ww",
                    Image = "string",
                }
            };

            _authService.Login(email, password).Returns(result);
            Assert.That(await _authService.Login(email, password), Is.EqualTo(result));
        }

        [Test]
        public async Task Login_WrongEmail_Throws_NotFoundException()
        {
            string email = "qw@ww.com";
            string password = "ww";

            //var exception = new NotFoundException($"User with email {email} doesnt exists! Register first.");
            //_authService.Login(email, password).Returns(exception);
            //Assert.That(await _authService.Login(email, password), Is.EqualTo(email));
             Assert.ThrowsAsync<NotFoundException>(async () => await _authService.Login(email, password));
            //StringAssert.Contains($"User with email {email} doesnt exists! Register first.", ex.Message.ToString());
        }
    }
}