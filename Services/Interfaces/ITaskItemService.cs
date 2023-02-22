using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;

namespace ToDoApi.Services.Interfaces
{
    public interface ITaskItemService
    {
        #region basic task
        Task<List<TaskItemDto>> GetAllAsync(CancellationToken ct = default);
        Task<TaskItemDto> GetAsync(Guid taskId, CancellationToken ct = default);
        Task<TaskItemDto> CreateAsync(TaskItemModel model, CancellationToken ct = default);
        Task<TaskItemDto> UpdateAsync(Guid taskId, TaskItemModel model, CancellationToken ct = default);
        Task DeleteAsync(Guid taskId, CancellationToken ct = default);
        #endregion

        #region tasks in tasklists
        Task<TaskItemDto> CreateAtTaskListAsync(Guid taskListId, TaskItemModel model, CancellationToken ct = default);
        Task<TaskItemDto> UpdateAtTaskListAsync(Guid taskListId, Guid taskId, TaskItemModel model, CancellationToken ct = default);
        Task<TaskItemDto> DeleteAtTaskListAsync(Guid taskListId, TaskItemModel model, CancellationToken ct = default);
        #endregion
    }
}