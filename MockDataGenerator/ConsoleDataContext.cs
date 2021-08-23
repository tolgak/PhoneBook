
using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.Repository;
using PhoneBook.Repository.Entities;

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

      optionsBuilder.UseNpgsql(configuration.GetConnectionString("DataConnection"));
    }

    public DbSet<Person> People { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
  }
}
