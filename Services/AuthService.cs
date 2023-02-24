using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDoApi.Database;
using ToDoApi.Entities.Domain;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
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
                throw new Exception($"User with email {user.Email} doesnt exists! Register first.");

            }

            var token = GenerateToken(user);
            var asd = new { Token = token, User = user };
            return asd;

        }


        public async Task<int> Register(User user, string Password)
        {
            if (await this.UserExists(user.Email))
            {
                throw new Exception($"user with mail {user.Email} exists");
            }

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