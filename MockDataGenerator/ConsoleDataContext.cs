using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;


using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;


using PhoneBook.Repository;
using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MockDataGenerator
{
  public class ConsoleDataContext : DbContext, IDataContext
  {

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
          .AddJsonFile("appsettings.json", false)
          .Build();

      optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Person> People { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
  }
}
