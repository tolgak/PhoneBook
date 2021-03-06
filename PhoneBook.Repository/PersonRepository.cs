using Microsoft.EntityFrameworkCore;
using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public class PersonRepository : IPersonRepository
  {

    private readonly IDataContext _ctx;

    public PersonRepository(IDataContext context)
    {
      _ctx = context;
    }

    public async Task Add(Person person)
    {
      if (person.Id == Guid.Empty)
        person.Id = Guid.NewGuid();

      _ctx.People.Add(person);
      await _ctx.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
      var itemToDelete = await _ctx.People.FindAsync(id);
      if (itemToDelete == null)
        throw new NullReferenceException("Can not delete: Person with given id not found");

      _ctx.People.Remove(itemToDelete);
      await _ctx.SaveChangesAsync();
    }

    public async Task<Person> Get(Guid id)
    {
      var entity = await _ctx.People.Include(x => x.ContactInfos).FirstOrDefaultAsync(x => x.Id == id);
      return entity;
    }

    public async Task<IEnumerable<Person>> GetAll()
    {
      var entities = await _ctx.People.Include(x => x.ContactInfos).ToListAsync();
      return entities;
    }

    public async Task Update(Guid id, Person person)
    {
      var itemToUpdate = await _ctx.People.FindAsync(id);
      if (itemToUpdate == null)
        throw new NullReferenceException("Can not update: Person with given id not found");

      itemToUpdate.First_Name = person.First_Name;
      itemToUpdate.Last_Name = person.Last_Name;
      itemToUpdate.Company = person.Company;

      await _ctx.SaveChangesAsync();
    }

  }
}
