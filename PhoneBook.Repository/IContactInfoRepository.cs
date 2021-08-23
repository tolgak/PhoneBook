using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public interface IContactInfoRepository
  {
    Task<ContactInfo> Get(Guid id);
    Task<IEnumerable<ContactInfo>> GetAll();
    Task Add(ContactInfo contactInfo);
    Task Delete(Guid id);
    Task Update(Guid id, ContactInfo contactInfo);

  }
}
