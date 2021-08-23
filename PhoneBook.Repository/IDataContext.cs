using Microsoft.EntityFrameworkCore;
using PhoneBook.Repository.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public interface IDataContext
  {
    DbSet<Person> People { get; set; }
    DbSet<ContactInfo> ContactInfos { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  }
}
