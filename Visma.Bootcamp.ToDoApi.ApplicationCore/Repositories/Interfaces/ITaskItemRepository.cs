using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<TaskItem> Get(Guid taskId);
        Task<List<TaskItem>> GetByTaskList(int taskListId);
        Task<TaskItem> Create(TaskItem taskItem);
        Task<TaskItem> Update(TaskItem taskItem);
        Task Delete(TaskItem taskItem);
    }
}