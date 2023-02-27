using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories.Interfaces;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationContext _context;

        public TaskItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> Create(TaskItem taskItem)
        {
            await _context.Tasks.AddAsync(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task Delete(TaskItem taskItem)
        {
            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskItem> Get(Guid taskId)
        {
            return await _context.Tasks.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskId);
        }

        public async Task<List<TaskItem>> GetByTaskList(int taskListId)
        {
            return await _context.Tasks.AsNoTracking().Where(t => t.TaskListId == taskListId).ToListAsync();
        }

        public async Task<TaskItem> Update(TaskItem taskItem)
        {
            _context.Tasks.Update(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }
    }
}