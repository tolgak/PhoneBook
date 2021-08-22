using Microsoft.EntityFrameworkCore;
using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public class ContactInfoRepository : IContactInfoRepository
  {

    public IDataContext _ctx;

    public ContactInfoRepository(IDataContext context)
    {
      _ctx = context;
    }

    public async Task Add(ContactInfo contactInfo)
    {
      if (contactInfo.Id == Guid.Empty)
        contactInfo.Id = Guid.NewGuid();

      _ctx.ContactInfos.Add(contactInfo);
      await _ctx.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
      var itemToDelete = await _ctx.ContactInfos.FindAsync(id);
      if (itemToDelete == null)
        throw new NullReferenceException("Can not delete: ContactInfo with given id not found");

      _ctx.ContactInfos.Remove(itemToDelete);
      await _ctx.SaveChangesAsync();
    }

    public async Task<ContactInfo> Get(Guid id)
    {
      var entity = await _ctx.ContactInfos.FindAsync(id);
      return entity;
    }

    public async Task<IEnumerable<ContactInfo>> GetAll()
    {
      var entities = await _ctx.ContactInfos.ToListAsync();
      return entities;
    }

    public async Task Update(Guid id, ContactInfo contactInfo)
    {
      var itemToUpdate = await _ctx.ContactInfos.FindAsync(id);
      if (itemToUpdate == null)
        throw new NullReferenceException("Can not update: ContactInfo with given id not found");

      itemToUpdate.ContactInfoType = contactInfo.ContactInfoType;
      itemToUpdate.Info = contactInfo.Info;

      await _ctx.SaveChangesAsync();
    }
  }
}
