using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;

namespace ToDoApi.Services.Interfaces
{
    public interface ITaskListService
    {
        Task<List<TaskListDto>> GetAllAsync(CancellationToken ct = default);

        Task<TaskListDto> GetByUserAsync(Guid userId, CancellationToken ct = default);
        Task<TaskListDto> GetAsync(Guid taskListId, CancellationToken ct = default);

        Task<TaskListDto> CreateAsync(TaskListModel model, CancellationToken ct = default);
        Task<TaskListDto> UpdateAsync(Guid taskListId, TaskItemModel model, CancellationToken ct = default);
        Task DeleteAsync(Guid taskListId, CancellationToken ct = default);
    }
}