using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                throw new NotFoundException($"User witdh Id {userId} doesnt exist!");
            }

            await _userRepository.Delete(user);
        }

        public async Task<UserDto> GetUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} does not exist");
            }
            return user.ToDto();
        }

        public async Task<UserDto> UpdateUserAsync(Guid userId, UserModel model, CancellationToken ct = default)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} does not exist");

            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Image = model.Image;
            user.Password = model.Password;

            await _userRepository.Update(user);
            return user.ToDto();
        }
    }
}