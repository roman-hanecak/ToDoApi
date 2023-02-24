using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;

namespace ToDoApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(Guid userId, CancellationToken ct = default);
        Task<UserDto> UpdateUserAsync(Guid userId, UserModel model, CancellationToken ct = default);
        Task DeleteUserAsync(Guid userId, CancellationToken ct = default);
    }
}