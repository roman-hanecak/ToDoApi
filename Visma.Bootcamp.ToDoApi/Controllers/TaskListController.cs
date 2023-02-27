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
    [Route("api/lists")]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskListService _taskListService;

        public TaskListController(ITaskListService taskListService)
        {
            _taskListService = taskListService;
        }



        [HttpGet("{task_list_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Retrieve taskList based on its Id",
            description: "Return taskList given by TaskListId",
            OperationId = "GetTaskIList",
            Tags = new[] { "TaskList API" })]
        public async Task<IActionResult> GetTaskListAsync(
            [Required, FromRoute(Name = "task_list_id")] Guid? taskListId,
            CancellationToken ct)
        {
            TaskListDto taskListDto = await _taskListService.GetAsync(taskListId.Value, ct);
            return Ok(taskListDto);
        }

        [HttpPost("{user_id}")]
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
            CancellationToken ct)
        {
            TaskListDto taskListDto = await _taskListService.CreateAsync(userId, model, ct);

            return CreatedAtRoute(
                new { task_item_id = taskListDto.PublicId },
                taskListDto);
        }

        [HttpDelete("{task_list_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Delete taskList",
            description: "Delete TaskList from the database",
            OperationId = "DeleteTaskList",
            Tags = new[] { "TaskList API" })]
        public async Task<IActionResult> DeleteTaskListAsync(
            [Required, FromRoute(Name = "task_list_id")] Guid? taskListId,
            CancellationToken ct)
        {
            await _taskListService.DeleteAsync(taskListId.Value, ct);
            return NoContent();
        }

        [HttpPut("{task_list_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            summary: "Update taskList",
            description: "Update taskList in the database",
            OperationId = "UpdateTaskList",
            Tags = new[] { "TaskList API" })]
        public async Task<IActionResult> UpdateTaskListAsync(
            [Required, FromRoute(Name = "task_list_id")] Guid? taskListId,
            [Bind, FromBody] TaskListModel model,
            CancellationToken ct)
        {
            TaskListDto taskListDto = await _taskListService.UpdateAsync(taskListId.Value, model, ct);
            return Ok(taskListDto);
        }


        [HttpGet("{task_list_id}/tasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Get all tasks for tasklist",
            description: "Get all tasks for tasklist",
            OperationId = "GetTasksByTaskListId",
            Tags = new[] { "TaskItem API" })]
        public async Task<IActionResult> GetAllTasksByTaskList(
            [Required, FromRoute(Name = "task_list_id")] Guid taskListId,
            [FromServices] ITaskItemService taskItemService,
            CancellationToken ct)
        {
            List<TaskItemDto> taskItemDtos = await taskItemService.GetTasksByTaskList(taskListId, ct);
            return Ok(taskItemDtos);
        }
    }
}