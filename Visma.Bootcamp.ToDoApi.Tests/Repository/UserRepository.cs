using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions;
using Visma.Bootcamp.ToDoApi.Tests.Repository.Interfaces;

namespace Visma.Bootcamp.ToDoApi.Tests.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _testContext;

        public UserRepository(ApplicationContext testContext)
        {
            _testContext = testContext;
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _testContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new NotFoundException($"User witdh Id {userId} doesnt exist!");
            }

            _testContext.Users.Remove(user);
            await _testContext.SaveChangesAsync();
        }

        public async Task<UserDto> GetUserAsync(Guid userId)
        {
            var user = await _testContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} does not exist");
            }
            return user.ToDto();
        }

        public async Task<UserDto> UpdateUserAsync(Guid userId, UserModel model)
        {
            var user = await _testContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} does not exist");

            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Image = model.Image;
            user.Password = model.Password;

            _testContext.Users.Update(user);
            await _testContext.SaveChangesAsync();
            return user.ToDto();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        void IUserRepository.Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}