using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.ToDoApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(
        [FromBody, Bind] UserModel request,
        CancellationToken ct)
        {
            try
            {
                var user = new User()
                {
                    PublicId = Guid.NewGuid(),
                    Email = request.Email,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Image = request.Image
                };
                var userId = await _authService.Register(user, request.Email);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(
            [FromBody, Bind] LoginModel request,
            CancellationToken ct)
        {
            try
            {

                var tuple = await _authService.Login(request.Email, request.Password);
                return StatusCode(StatusCodes.Status200OK, tuple);
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}