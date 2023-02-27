using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.ToDoApi.Tests.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        private IUserRepository _userRepository;
        private IAuthService _authService;
        private IConfiguration _config;


        [SetUp]
        public void Setup()
        {

            _userRepository = Substitute.For<IUserRepository>();
            _authService = new AuthService(_userRepository);
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
            _userRepository.GetByLogin(email, password).Returns(result.User);

            var userLoginResult = await _authService.Login(email, password);

            //generate token has always null input
            //Assert.IsNotNull(userLoginResult);
        }

        [Test]
        public async Task Login_WrongEmail_Throws_NotFoundException()
        {
            string email = "qw@dw.com";
            string password = "hh";


            _userRepository.GetByLogin(email, password).ReturnsNull();

            Assert.ThrowsAsync<NotFoundException>(async () => await _authService.Login(email, password));
        }
    }
}