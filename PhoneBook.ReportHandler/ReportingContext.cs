
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.Repository;
using PhoneBook.Repository.Entities;
using System;
using System.IO;

namespace PhoneBook.ReportHandler
{

  public class ReportingContext : DbContext
  {

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
          .AddJsonFile("appsettings.json", false)
          .Build();

      optionsBuilder.UseNpgsql(configuration.GetConnectionString("ReportingConnection"), b => b.MigrationsAssembly("PhoneBook.ReportHandler"));
    }

    //public DbSet<Person> People { get; set; }
    //public DbSet<ContactInfo> ContactInfos { get; set; }

    public DbSet<ReportRequest> ReportRequests  { get; set; }


  }

}
