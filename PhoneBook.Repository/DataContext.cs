using Microsoft.EntityFrameworkCore;
using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public class DataContext : DbContext, IDataContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
      
    }

    public DbSet<Person> People { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ContactInfo>()
          .HasOne<Person>(s => s.Person)
          .WithMany(g => g.ContactInfos)
          .HasForeignKey(x => x.PersonId);
    }
  }
}
