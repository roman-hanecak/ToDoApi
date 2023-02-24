using ToDoApi.Database;
using System.Configuration;
using MySql.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySql.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Services.Interfaces;
using ToDoApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEntityFrameworkMySQL().AddDbContext<ApplicationContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securitySchema = new OpenApiSecurityScheme()
    {
        Description = "JWT Auth Bearer Scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Reference = new OpenApiReference()
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    var securityRequirement = new OpenApiSecurityRequirement
{
    {
        securitySchema,
        new[] {"Bearer"}
    }
};
    c.AddSecurityDefinition("Bearer", securitySchema);
    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddTransient<ITaskItemService, TaskItemService>();
builder.Services.AddTransient<ITaskListService, TaskListService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
