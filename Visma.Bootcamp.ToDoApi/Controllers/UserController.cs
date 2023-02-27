using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.ToDoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpDelete("{user_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Delete user",
            description: "Delete user from the database",
            OperationId = "DeleteUser",
            Tags = new[] { "User API" })]
        public async Task<IActionResult> DeleteUserAsync(
            [Required, FromRoute(Name = "user_id")] Guid? userId,
            CancellationToken ct)
        {
            await _userService.DeleteUserAsync(userId.Value, ct);
            return NoContent();
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
            [Required, FromRoute(Name = "user_id")] Guid? userId,
            CancellationToken ct)
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

        [HttpGet("{user_id}/lists")]
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

        [HttpPost("{user_id}/lists")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskListDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            summary: "Create new TaskList for User",
            description: "Create new TaskList in the database",
            OperationId = "CreateTaskList",
            Tags = new[] { "TaskList API" })]
        public async Task<IActionResult> CreateTaskListAsync(
            [Required, FromRoute(Name = "user_id")] Guid userId,
            [FromBody, Bind] TaskListModel model,
            [FromServices] ITaskListService taskListService,
            CancellationToken ct)
        {
            TaskListDto taskListDto = await taskListService.CreateAsync(userId, model, ct);

            return CreatedAtRoute(
                new { task_item_id = taskListDto.PublicId },
                taskListDto);
        }
    }
}