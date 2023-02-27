using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories.Interfaces
{
    public interface ITaskListRepository
    {
        Task<List<TaskList>> GetByUser(int userId);
        Task<TaskList> Get(Guid taskListId);
        Task<TaskList> Create(TaskList taskList);
        Task<TaskList> Update(TaskList taskList);
        Task Delete(TaskList taskList);
    }
}