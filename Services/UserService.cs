using Microsoft.EntityFrameworkCore;
using ToDoApi.Database;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;
using ToDoApi.Exceptions;
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


        public async Task DeleteUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new NotFoundException($"User witdh Id {userId} doesnt exist!");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDto> GetUserAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} does not exist");
            }
            return user.ToDto();
        }

        public async Task<UserDto> UpdateUserAsync(Guid userId, UserModel model, CancellationToken ct = default)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} does not exist");
                
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

    }
}
