using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PhoneBook.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace PhoneBook.Repository
{
  public interface IDataContext
  {
    DbSet<Person> People { get; set; }
    DbSet<ContactInfo> ContactInfos { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  }
}
