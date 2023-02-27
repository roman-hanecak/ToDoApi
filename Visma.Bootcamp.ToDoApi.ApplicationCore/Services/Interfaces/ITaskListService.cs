using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces
{
    public interface ITaskListService
    {
        Task<List<TaskListDto>> GetByUserAsync(Guid userId, CancellationToken ct = default);
        Task<TaskListDto> GetAsync(Guid taskListId, CancellationToken ct = default);
        Task<TaskListDto> CreateAsync(Guid userId, TaskListModel model, CancellationToken ct = default);
        Task<TaskListDto> UpdateAsync(Guid taskListId, TaskListModel model, CancellationToken ct = default);
        Task DeleteAsync(Guid taskListId, CancellationToken ct = default);
    }
}