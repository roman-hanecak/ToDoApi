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

        // public async Task<TaskItemDto> CreateAsync(TaskItemModel model, CancellationToken ct = default)
        // {

        //     var taskItem = new TaskItem
        //     {
        //         PublicId = Guid.NewGuid(),
        //         Title = model.Title,
        //         Description = model.Description,
        //         CreatedDate = DateTime.UtcNow,
        //         EndDate = model.EndDate,
        //         Completed = false
        //     };
        //     //System.Console.WriteLine(taskItem);
        //     await _context.Tasks.AddAsync(taskItem);
        //     await _context.SaveChangesAsync();

        //     return taskItem.ToDto();
        // }

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
            //model.ToDomain();
            // taskItem.PublicId = Guid.NewGuid();
            // taskItem.CreatedDate = DateTime.UtcNow;
            // taskItem.Completed = false;
            // taskItem.Id = taskList.Id;
            //taskItem.TaskListId = taskListId;

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



        public async Task<List<TaskItemDto>> GetAllAsync(CancellationToken ct = default)
        {
            var tasks = await _context.Tasks.AsNoTracking().ToListAsync(ct);

            List<TaskItemDto> taskItemDtos = tasks.Select(x => x.ToDto()).ToList();

            return taskItemDtos;
            //throw new NotImplementedException();
        }

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

        public async Task<TaskItemDto> UpdateAtTaskListAsync(Guid taskListId, Guid taskId, TaskItemModel model, CancellationToken ct = default)
        {
            // var taskList = await _context.TaskLists.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskListId);

            // if(taskList == null){
            //     throw new Exception($"List with given Id {taskListId} wasnt found!");
            // }

            // var taskItem = await _context.TaskLists.AsNoTracking()
            throw new NotImplementedException();

        }

        public Task<TaskItemDto> DeleteAtTaskListAsync(Guid taskListId, TaskItemModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}