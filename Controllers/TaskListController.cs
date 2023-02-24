using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskListService _taskListService;

        public TaskListController(ITaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        // [HttpGet]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskListDto>))]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [SwaggerOperation(
        //     summary: "Get All existing TaskLists",
        //     description: "Get all TaskLists from database",
        //     OperationId = "GetTaskLists",
        //     Tags = new[] { "TaskList API" })]
        // public async Task<IActionResult> GetAllTasksAsync(CancellationToken ct)
        // {
        //     List<TaskListDto> taskListDtos = await _taskListService.GetAllAsync(ct: ct);
        //     return Ok(taskListDtos);
        // }

        [HttpGet("{task_list_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskListDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            summary: "Retrieve taskList based on its Id",
            description: "Return taskList given by TaskListId",
            OperationId = "GetTaskIList",
            Tags = new[] { "TaskList API" })]
        public async Task<IActionResult> GetTaskListAsync(
            [Required, FromRoute(Name = "task_list_id")] Guid? taskListId, CancellationToken ct)
        {
            TaskListDto taskListDto = await _taskListService.GetAsync(taskListId.Value, ct);
            return Ok(taskListDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskListDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            summary: "Create new TaskList for User",
            description: "Create new TaskList in the database",
            OperationId = "CreateTaskList",
            Tags = new[] { "TaskList API" })]
        public async Task<IActionResult> CreateTaskListAsync(Guid userId, [FromBody, Bind] TaskListModel model, CancellationToken ct)
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
        public async Task<IActionResult> DeleteTaskListAsync([Required, FromRoute(Name = "task_list_id")] Guid? taskListId, CancellationToken ct)
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


        // [HttpPost("{task_list_id}/TaskItems")]
        // [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskItemDto))]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [SwaggerOperation(
        //     summary: "Create TaskItem in existing TaskList",
        //     description: "Create TaskItem in the database and associate it with TaskList",
        //     OperationId = "CreateTaskItemWithTaskListId",
        //     Tags = new[] { "TaskItem API" })]
        // public async Task<IActionResult> AddTaskToTaskListAsync(
        //     [Required, FromRoute(Name = "task_list_id")] Guid? taskListId,
        //     [Bind, FromBody] TaskItemModel model,
        //     [FromServices] ITaskItemService taskItemService,
        //     CancellationToken ct)
        // {
        //     TaskItemDto taskItemDto = await taskItemService.CreateAsync(taskListId.Value, model, ct);
        //     return CreatedAtRoute(
        //         new { task_list_id = taskItemDto.PublicId },
        //         taskItemDto);
        // }

        [HttpGet("{task_list_id}/TaskItems")]
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