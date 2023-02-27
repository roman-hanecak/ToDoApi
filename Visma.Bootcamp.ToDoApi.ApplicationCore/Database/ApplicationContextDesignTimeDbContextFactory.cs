using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Database
{
    public class ApplicationContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {



        public ApplicationContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            DbContextOptionsBuilder<ApplicationContext> builder = new DbContextOptionsBuilder<ApplicationContext>().UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                opts => opts.MigrationsAssembly("Visma.Bootcamp.ToDoApi.ApplicationCore"));

            return new ApplicationContext(builder.Options);
        }
    }
}
