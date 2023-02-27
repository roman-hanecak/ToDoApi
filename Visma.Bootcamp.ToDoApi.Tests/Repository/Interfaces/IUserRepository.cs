using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;

namespace Visma.Bootcamp.ToDoApi.Tests.Repository.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        Task<UserDto> GetUserAsync(Guid userId);
        Task<UserDto> UpdateUserAsync(Guid userId, UserModel model);
        Task DeleteUserAsync(Guid userId);

        protected void Dispose(bool disposing);
    }
}