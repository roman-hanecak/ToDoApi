using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(Guid userId, CancellationToken ct = default);
        Task<UserDto> UpdateUserAsync(Guid userId, UserModel model, CancellationToken ct = default);
        Task DeleteUserAsync(Guid userId, CancellationToken ct = default);
    }
}