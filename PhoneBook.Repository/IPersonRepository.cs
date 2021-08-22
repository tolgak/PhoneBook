using PhoneBook.Dto;
using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public interface IPersonRepository
  {
    Task<Person> Get(Guid id);
    Task<IEnumerable<Person>> GetAll();
    Task Add(Person person);
    Task Delete(Guid id);
    Task Update(Guid id, Person person);
  }


}
