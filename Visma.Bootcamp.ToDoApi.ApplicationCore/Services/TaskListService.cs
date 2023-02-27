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
using Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories.Interfaces;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IUserRepository _userRepository;

        public TaskListService(ITaskListRepository taskListRepository, IUserRepository userRepository = null)
        {
            _taskListRepository = taskListRepository;
            _userRepository = userRepository;
        }

        public async Task<TaskListDto> CreateAsync(Guid userId, TaskListModel model, CancellationToken ct = default)
        {

            var user = await _userRepository.Get(userId);
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
            await _taskListRepository.Create(taskList);

            return taskList.ToDto();
        }

        public async Task DeleteAsync(Guid taskListId, CancellationToken ct = default)
        {
            var taskList = await _taskListRepository.Get(taskListId);

            if (taskList == null)
            {
                throw new NotFoundException($"Tasklist with Id {taskListId} doesnt exists!");
            }

            await _taskListRepository.Delete(taskList);
        }


        public async Task<TaskListDto> GetAsync(Guid taskListId, CancellationToken ct = default)
        {
            var taskList = await _taskListRepository.Get(taskListId);
            if (taskList == null)
            {
                throw new NotFoundException($"Task list with id {taskListId} doesnt exists!");
            }
            return taskList.ToDto();
        }


        public async Task<List<TaskListDto>> GetByUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} wasnt found!");
            }

            var taskList = await _taskListRepository.GetByUser(user.Id);
            if (taskList == null)
            {
                throw new NotFoundException("TaskLists werent found!");
            }

            List<TaskListDto> taskListDtos = taskList.Select(x => x.ToDto()).ToList();

            return taskListDtos;
        }


        public async Task<TaskListDto> UpdateAsync(Guid taskListId, TaskListModel model, CancellationToken ct = default)
        {
            var taskList = await _taskListRepository.Get(taskListId);
            if (taskList == null)
            {
                throw new NotFoundException($"Task list with id {taskListId} doesnt exists!");
            }

            taskList.Title = model.Title;

            await _taskListRepository.Update(taskList);

            return taskList.ToDto();
        }


    }
}