using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bookStory.Data.EF
{
    public class bookStoryDbContextFactory : IDesignTimeDbContextFactory<bookStoryDbContext>
    {
        public bookStoryDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("bookStoryDb");
            var optionsBuilder = new DbContextOptionsBuilder<bookStoryDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new bookStoryDbContext(optionsBuilder.Options);
        }
    }
}