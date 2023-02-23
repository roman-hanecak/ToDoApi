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
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskItemDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Get All existing TaskItems",
            description: "Get all TaskItems from database",
            OperationId = "CreateTaskItem",
            Tags = new[] { "TaskItem API" })]
        //public async Task<List<TaskItemDto>> GetAllAsync(CancellationToken ct = default)
        public async Task<IActionResult> GetAllTasksAsync(CancellationToken ct)
        {
            List<TaskItemDto> taskItemList = await _taskItemService.GetAllAsync(ct: ct);
            return Ok(taskItemList);
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
            [Required, FromRoute(Name = "task_item_id")] Guid? taskItemId, CancellationToken ct)
        {

            TaskItemDto taskItemDto = await _taskItemService.GetAsync(taskItemId.Value, ct);
            return Ok(taskItemDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskItemDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            summary: "Create new TaskItem",
            description: "Create new TaskItem in the database",
            OperationId = "CreateTaskItem",
            Tags = new[] { "TaskItem API" })]
        public async Task<IActionResult> CreateTaskItemAsync(Guid taskListId, [FromBody, Bind] TaskItemModel model, CancellationToken ct)
        {
            TaskItemDto taskItemDto = await _taskItemService.CreateAsync(taskListId, model, ct);


            return CreatedAtRoute(
                new { task_item_id = taskItemDto.PublicId },
                taskItemDto);
        }

        [HttpDelete("{task_item_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Delete taskItem",
            description: "Delete TaskItem from the database",
            OperationId = "DeleteTaskItem",
            Tags = new[] { "TaskItem API" })]
        public async Task<IActionResult> DeleteTaskItemAsync([Required, FromRoute(Name = "task_item_id")] Guid? taskItemId, CancellationToken ct)
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