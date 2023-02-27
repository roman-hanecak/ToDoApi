using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories.Interfaces;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(Guid id);
        Task<User> GetByMail(string mail);
        Task<User> GetByLogin(string mail, string password);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task Delete(User user);
    }
}