using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Database;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, ApplicationContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<Object> Login(string email, string password)
        {
            var user = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(w => w.Email.ToLower() == email.ToLower() && w.Password == password);
            if (user == null)
            {
                throw new NotFoundException($"User with email {user.Email} doesnt exists! Register first.");
            }
            // if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            // {
            //     throw new NotValidException("Invalid password!");
            // }

            var token = GenerateToken(user);
            var asd = new { Token = token, User = user };
            return asd;

        }


        public async Task<int> Register(User user, string Password)
        {
            if (await this.UserExists(user.Email))
            {
                throw new ConflictException($"user with mail {user.Email} exists");
            }

            var isMailValid = MailAddress.TryCreate(user.Email, out MailAddress? result);

            if (!isMailValid)
            {
                throw new NotValidException($"Email address {user.Email} is not valid!");
            }

            // this.CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);
            // user.PasswordHash = passwordHash;
            // user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var tokenKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
                Audience = audience,
                Issuer = issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if (user is null)
            {
                return false;
            }
            return true;
        }
    }
}