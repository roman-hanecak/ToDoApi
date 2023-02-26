using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Database;

namespace ToDoApi.Tests.Database
{
    public class TestApplicationContext : ApplicationContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase($"{nameof(TestApplicationContext)}-{Guid.NewGuid()}");
        }
    }

}