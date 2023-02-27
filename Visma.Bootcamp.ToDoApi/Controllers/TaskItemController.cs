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
    [Route("api/tasks")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpGet("{task_item_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Retrieve taskItem based on its Id",
            description: "Return TaskItem given by TaskItemId",
            OperationId = "GetTaskItem",
            Tags = new[] { "TaskItem API" })]
        public async Task<IActionResult> GetTaskItemAsync(
            [Required, FromRoute(Name = "task_item_id")] Guid? taskItemId,
            CancellationToken ct)
        {

            TaskItemDto taskItemDto = await _taskItemService.GetAsync(taskItemId.Value, ct);
            return Ok(taskItemDto);
        }

        [HttpDelete("{task_item_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Delete taskItem",
            description: "Delete TaskItem from the database",
            OperationId = "DeleteTaskItem",
            Tags = new[] { "TaskItem API" })]
        public async Task<IActionResult> DeleteTaskItemAsync(
            [Required, FromRoute(Name = "task_item_id")] Guid? taskItemId,
            CancellationToken ct)
        {
            await _taskItemService.DeleteAsync(taskItemId.Value, ct);
            return NoContent();
        }

        [HttpPut("{task_item_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            summary: "Update taskItem",
            description: "Update taskItem in the database",
            OperationId = "UpdateTaskItem",
            Tags = new[] { "TaskItem API" })]
        public async Task<IActionResult> UpdateTaskItemAsync(
            [Required, FromRoute(Name = "task_item_id")] Guid? taskItemId,
            [Bind, FromBody] TaskItemModel model,
            CancellationToken ct)
        {
            TaskItemDto taskItemDto = await _taskItemService.UpdateAsync(taskItemId.Value, model, ct);
            return Ok(taskItemDto);
        }
    }
}