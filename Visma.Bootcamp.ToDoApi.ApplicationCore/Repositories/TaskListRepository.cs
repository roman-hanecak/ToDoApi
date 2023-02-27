using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories.Interfaces
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly ApplicationContext _context;

        public TaskListRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TaskList> Create(TaskList taskList)
        {
            _context.TaskLists.Add(taskList);
            await _context.SaveChangesAsync();
            return taskList;
        }

        public async Task Delete(TaskList taskList)
        {
            _context.TaskLists.Remove(taskList);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskList> Get(Guid taskListId)
        {
            return await _context.TaskLists.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskListId);
        }

        public async Task<List<TaskList>> GetByUser(int userId)
        {
            return await _context.TaskLists.AsNoTracking().Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TaskList> Update(TaskList taskList)
        {
            _context.TaskLists.Update(taskList);
            await _context.SaveChangesAsync();
            return taskList;
        }
    }
}