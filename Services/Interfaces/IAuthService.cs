using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Entities.Domain;
using ToDoApi.Entities.Model;

namespace ToDoApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<int> Register(User user, string Password);
        Task<string> Login(string userName , string password);
        Task<bool> UserExists(string username);
    }
}