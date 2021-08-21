using PhoneBook.Dto;
using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Repository
{
  public class PersonRepository : IPersonRepository
  {

    private IDataContext _ctx;
    private IMapper _mapper1;
    private IMapper _mapper2;

    public PersonRepository(IDataContext context)
    {
      _ctx = context;
      var config1 = new MapperConfiguration(cfg => cfg.CreateMap<dtoPerson, Person>());
      _mapper1 = config1.CreateMapper();

      var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Person, dtoPerson>());
      _mapper2 = config2.CreateMapper();
    }

    public async Task Add(dtoPerson person)
    {
      if (person.Id == Guid.Empty)
        person.Id = Guid.NewGuid();

      var entity = _mapper1.Map<Person>(person);

      _ctx.People.Add(entity);
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

    public async Task<dtoPerson> Get(Guid id)
    {
      var entity = await _ctx.People.FindAsync(id);
      return _mapper2.Map<dtoPerson>(entity);
    }

    public async Task<IEnumerable<dtoPerson>> GetAll()
    {
      var entities = await _ctx.People.ToListAsync();
      return _mapper2.Map<List<dtoPerson>>(entities);
    }

    public async Task Update(Guid id, dtoPerson person)
    {
      var itemToUpdate = await _ctx.People.FindAsync(id);
      if (itemToUpdate == null)
        throw new NullReferenceException("Can not update: Person with given id not found");

      _ctx.People.Remove(itemToUpdate);
      _ctx.People.Add(_mapper1.Map<Person>(person));

      await _ctx.SaveChangesAsync();

    }
  }
}
