using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;

namespace Visma.Bootcamp.ToDoApi.Tests.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<int> Register(User user, string password);
        Task<Object> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}