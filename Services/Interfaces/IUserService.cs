using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;

namespace ToDoApi.Services.Interfaces
{
    public interface IUserService
    {
        //Task<List<UserDto>> GetAllUsersAsync(CancellationToken ct = default);

        Task<UserDto> GetUserAsync(Guid userId, CancellationToken ct = default);

        //Task<UserDto> CreateUserAsync(UserModel model, CancellationToken ct = default);
        Task<UserDto> UpdateUserAsync(Guid userId, UserModel model, CancellationToken ct = default);
        Task DeleteUserAsync(Guid userId, CancellationToken ct = default);
        //Task<UserDto> LoginUserAsync(string email, string password, CancellationToken ct = default);
    }
}