using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories.Interfaces;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<User> Get(Guid Id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.PublicId == Id);
        }

        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByMail(string mail)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == mail.ToLower());
        }

        public async Task<User> GetByLogin(string mail, string password)
        {
            return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(w => w.Email.ToLower() == mail.ToLower() && w.Password == password);
        }

        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}