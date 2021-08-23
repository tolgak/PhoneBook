using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public interface IReportRequestRepository
  {
    Task Add(Location location);
  }
}
