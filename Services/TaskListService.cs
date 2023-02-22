using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Database;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ApplicationContext _context;

        public TaskListService(ApplicationContext context)
        {
            _context = context;
        }

        public Task<TaskListDto> CreateAsync(TaskListModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid taskListId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskListDto>> GetAllAsync(CancellationToken ct = default)
        {
           var taskLists = await _context.TaskLists.AsNoTracking().ToListAsync(ct);

            List<TaskListDto> taskListDtos = taskLists.Select(x => x.ToDto()).ToList();

            return taskListDtos;
            //throw new NotImplementedException();
        }

        public Task<TaskListDto> GetAsync(Guid taskListId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<TaskListDto> GetByUserAsync(Guid userId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<TaskListDto> UpdateAsync(Guid taskListId, TaskItemModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}