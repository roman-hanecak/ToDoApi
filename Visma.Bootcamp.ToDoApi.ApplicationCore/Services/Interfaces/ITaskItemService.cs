using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItemDto> GetAsync(Guid taskId, CancellationToken ct = default);
        Task<List<TaskItemDto>> GetTasksByTaskList(Guid taskListId, CancellationToken ct = default);
        Task<TaskItemDto> CreateAsync(Guid taskListId, TaskItemModel model, CancellationToken ct = default);
        Task<TaskItemDto> UpdateAsync(Guid taskId, TaskItemModel model, CancellationToken ct = default);
        Task DeleteAsync(Guid taskId, CancellationToken ct = default);
    }
}