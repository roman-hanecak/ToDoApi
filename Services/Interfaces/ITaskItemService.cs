using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;

namespace ToDoApi.Services.Interfaces
{
    public interface ITaskItemService
    {
        
        Task<TaskItemDto> GetAsync(Guid taskId, CancellationToken ct = default);
        Task<List<TaskItemDto>> GetTasksByTaskList(Guid taskListId, CancellationToken ct = default);
        Task<TaskItemDto> CreateAsync(Guid taskListId,TaskItemModel model, CancellationToken ct = default);
        Task<TaskItemDto> UpdateAsync(Guid taskId, TaskItemModel model, CancellationToken ct = default);
        Task DeleteAsync(Guid taskId, CancellationToken ct = default);
        

    }
}