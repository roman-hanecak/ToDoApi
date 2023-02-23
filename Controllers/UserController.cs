using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            summary: "Create new user",
            description: "Create new user in the database",
            OperationId = "CreateUser",
            Tags = new[] { "User API" })]
        public async Task<IActionResult> CreateUserAsync([FromBody, Bind] UserModel model, CancellationToken ct)
        {
            UserDto userDto = await _userService.CreateUserAsync(model, ct);

            return CreatedAtRoute(
                new { user_id = userDto.PublicId },
                userDto);
        }

        [HttpDelete("{user_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Delete user",
            description: "Delete user from the database",
            OperationId = "DeleteUser",
            Tags = new[] { "User API" })]
        public async Task<IActionResult> DeleteUserAsync([Required, FromRoute(Name = "user_id")] Guid? userId, CancellationToken ct)
        {
            await _userService.DeleteUserAsync(userId.Value, ct);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Get all users",
            description: "Get all users from database",
            OperationId = "GetAllUsers",
            Tags = new[] { "User API" })]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken ct)
        {
            List<UserDto> userList = await _userService.GetAllUsersAsync();
            return Ok(userList);
        }

        [HttpGet("{user_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Retrieve user based on its Id",
            description: "Return user given by userId",
            OperationId = "GetUser",
            Tags = new[] { "User API" })]
        public async Task<IActionResult> GetUserAsync(
            [Required, FromRoute(Name = "user_id")] Guid? userId, CancellationToken ct)
        {

            UserDto userDto = await _userService.GetUserAsync(userId.Value, ct);
            return Ok(userDto);
        }

        [HttpPut("{user_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            summary: "Update user",
            description: "Update user in the database",
            OperationId = "UpdateUser",
            Tags = new[] { "User API" })]
        public async Task<IActionResult> UpdateUserAsync(
            [Required, FromRoute(Name = "user_id")] Guid? userId,
            [Bind, FromBody] UserModel model,
            CancellationToken ct)
        {
            UserDto userDto = await _userService.UpdateUserAsync(userId.Value, model, ct);
            return Ok(userDto);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            summary: "Create new user",
            description: "Create new user in the database",
            OperationId = "CreateUser",
            Tags = new[] { "User API" })]
        public async Task<IActionResult> LoginAsync([FromBody, Bind] LoginModel model, CancellationToken ct)
        {
            UserDto userDto = await _userService.LoginUserAsync(model.Email, model.Password, ct);

            return Ok(userDto);
        }

        [HttpGet("{user_id}/TaskLists")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Get all tasklists for user",
            description: "Get all tasklists for user",
            OperationId = "GetTaskListsByUserId",
            Tags = new[] { "TaskList API" })]
        public async Task<IActionResult> GetAllTaskListsByUser(
            [Required, FromRoute(Name = "user_id")] Guid userId,
            [FromServices] ITaskListService taskListService,
            CancellationToken ct)
        {
            List<TaskListDto> taskListDtos = await taskListService.GetByUserAsync(userId, ct);
            return Ok(taskListDtos);
        }
    }
}
