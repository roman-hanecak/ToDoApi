using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;

namespace Visma.Bootcamp.ToDoApi.Tests.Repository.Interfaces
{
    public class IUserRepository : IDisposable
    {
        public readonly ApplicationContext _context;
        Task<UserDto> GetUserAsync(Guid userId);
        Task<UserDto> UpdateUserAsync(Guid userId, UserModel model);
        Task DeleteUserAsync(Guid userId);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}