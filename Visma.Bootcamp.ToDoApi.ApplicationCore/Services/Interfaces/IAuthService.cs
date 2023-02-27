using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces
{
    public interface IAuthService
    {
        Task<int> Register(User user, string password);
        Task<Object> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}