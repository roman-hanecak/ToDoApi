using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ApplicationContext _context;

        public TaskListService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TaskListDto> CreateAsync(Guid userId, TaskListModel model, CancellationToken ct = default)
        {

            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} doesnt exists!");
            }

            var taskList = new TaskList
            {
                PublicId = Guid.NewGuid(),
                Title = model.Title,
                UserId = user.Id
            };
            await _context.TaskLists.AddAsync(taskList);
            await _context.SaveChangesAsync();

            return taskList.ToDto();
        }

        public async Task DeleteAsync(Guid taskListId, CancellationToken ct = default)
        {
            var taskList = await _context.TaskLists.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskListId);

            if (taskList == null)
            {
                throw new NotFoundException($"Tasklist with Id {taskListId} doesnt exists!");
            }

            _context.TaskLists.Remove(taskList);
            await _context.SaveChangesAsync();
        }


        public async Task<TaskListDto> GetAsync(Guid taskListId, CancellationToken ct = default)
        {
            var taskList = await _context.TaskLists.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskListId);
            if (taskList == null)
            {
                throw new NotFoundException($"Task list with id {taskListId} doesnt exists!");
            }
            return taskList.ToDto();
        }


        public async Task<List<TaskListDto>> GetByUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} wasnt found!");
            }

            var taskList = await _context.TaskLists.AsNoTracking().Where(t => t.UserId == user.Id).ToListAsync();
            if (taskList == null)
            {
                throw new NotFoundException("TaskLists werent found!");
            }

            List<TaskListDto> taskListDtos = taskList.Select(x => x.ToDto()).ToList();

            return taskListDtos;
        }


        public async Task<TaskListDto> UpdateAsync(Guid taskListId, TaskListModel model, CancellationToken ct = default)
        {
            var taskList = await _context.TaskLists.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == taskListId);
            if (taskList == null)
            {
                throw new NotFoundException($"Task list with id {taskListId} doesnt exists!");
            }

            taskList.Title = model.Title;

            _context.TaskLists.Update(taskList);
            await _context.SaveChangesAsync(ct);

            return taskList.ToDto();
        }


    }
}