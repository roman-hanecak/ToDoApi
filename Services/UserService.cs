using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Database;
using ToDoApi.Entities.Domain;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserDto> CreateUserAsync(UserModel model, CancellationToken ct = default)
        {
            var newUser = new User
            {
                PublicId = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Image = model.Image != null ? model.Image : "",
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser.ToDto();
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new Exception($"User witdh Id {userId} doesnt exist!");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync(CancellationToken ct = default)
        {
            var users = await _context.Users.AsNoTracking().ToListAsync(ct);
            List<UserDto> userDtos = users.Select(x => x.ToDto()).ToList();

            return userDtos;
        }

        public async Task<UserDto> GetUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new Exception($"User with Id {userId} does not exist");
            }
            return user.ToDto();
        }

        public async Task<UserDto> UpdateUserAsync(Guid userId, UserModel model, CancellationToken ct = default)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new Exception($"User with Id {userId} does not exist");
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Image = model.Image;
            user.Password = model.Password;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user.ToDto();
        }

        public async Task<UserDto> LoginUserAsync(string email, string password, CancellationToken ct)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (user == null)
            {
                throw new Exception($"User with such credentials does not exist");
            }
            return user.ToDto();

        }
    }
}
