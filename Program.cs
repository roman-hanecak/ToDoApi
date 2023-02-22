using ToDoApi.Database;
using System.Configuration;
using MySql.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySql.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Services.Interfaces;
using ToDoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEntityFrameworkMySQL().AddDbContext<ApplicationContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITaskItemService, TaskItemService>();
builder.Services.AddTransient<ITaskListService, TaskListService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
