using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Database;
using ToDoApi.Entities.Domain;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class TaskItemService : ITaskItemService
    {

        private readonly ApplicationContext _context;

        public TaskItemService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TaskItemDto> CreateAsync(Guid taskListId, TaskItemModel model, CancellationToken ct = default)
        {
            var taskList = await _context.TaskLists.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskListId);
            if (taskList == null)
            {
                throw new Exception($"TaskList with Id: {taskListId} doesnt exists!");
            }

            var taskItem = new TaskItem
            {
                PublicId = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                CreatedDate = DateTime.UtcNow,
                EndDate = model.EndDate,
                Completed = false,
                TaskListId = taskList.Id
            };

            await _context.Tasks.AddAsync(taskItem, ct);
            await _context.SaveChangesAsync(ct);
            return taskItem.ToDto();
        }

        public async Task DeleteAsync(Guid taskId, CancellationToken ct = default)
        {
            var taskItem = await _context.Tasks.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskId);
            if (taskItem == null)
            {
                throw new Exception($"Task with Id {taskId} doesnt exists!");
            }

            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();

        }



        // public async Task<List<TaskItemDto>> GetAllAsync(CancellationToken ct = default)
        // {
        //     var tasks = await _context.Tasks.AsNoTracking().ToListAsync(ct);

        //     List<TaskItemDto> taskItemDtos = tasks.Select(x => x.ToDto()).ToList();

        //     return taskItemDtos;
        //     //throw new NotImplementedException();
        // }

        public async Task<TaskItemDto> GetAsync(Guid taskId, CancellationToken ct = default)
        {
            var task = await _context.Tasks.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskId);
            if (task == null)
            {
                throw new Exception($"Task with {taskId} wasnt found");
            }
            return task.ToDto();
            //throw new NotImplementedException();
        }

        public async Task<TaskItemDto> UpdateAsync(Guid taskId, TaskItemModel model, CancellationToken ct = default)
        {
            var taskItem = await _context.Tasks.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskId);
            if (taskItem == null)
            {
                throw new Exception($"Task with Id {taskId} doesnt exists!");
            }
            taskItem.Title = model.Title;
            taskItem.Description = model.Description;
            taskItem.EndDate = model.EndDate;
            taskItem.Completed = model.Completed;

            _context.Tasks.Update(taskItem);
            await _context.SaveChangesAsync(ct);

            return taskItem.ToDto();
            //throw new NotImplementedException();
        }

        public async Task<List<TaskItemDto>> GetTasksByTaskList(Guid taskListId, CancellationToken ct = default)
        {
            var taskList = await _context.TaskLists.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskListId);
            if (taskList == null)
            {
                throw new Exception($"TaskList with Id {taskListId} wasnt found!");
            }
            //var query = db.Students.Include(s => s.Enrollments.Select(e => e.Course));
            var tasks = await _context.Tasks.AsNoTracking().Where(t => t.TaskListId == taskList.Id).ToListAsync();
            // Include(x => x.Products)
            //     .SingleOrDefaultAsync(x => x.PublicId == catalogId, ct);
            if (taskList == null)
            {
                throw new Exception("Tasks werent found!");
            }
            List<TaskItemDto> taskItems = tasks.Select(x => x.ToDto()).ToList();
            return taskItems;
        }
    }
}