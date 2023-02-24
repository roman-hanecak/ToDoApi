using ToDoApi.Entities.Domain;

namespace ToDoApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<int> Register(User user, string password);
        Task<Object> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}