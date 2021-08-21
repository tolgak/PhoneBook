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
    Task<dtoPerson> Get(Guid id);
    Task<IEnumerable<dtoPerson>> GetAll();
    Task Add(dtoPerson person);
    Task Delete(Guid id);
    Task Update(Guid id, dtoPerson person);
  }


}
