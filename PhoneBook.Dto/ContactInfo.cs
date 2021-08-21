using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Dto
{
  public class ContactInfo
  {
    public Guid Id { get; set; }
    public ContactInfoType ContactInfoType { get; set; }
    public string Info { get; set; }
  }
}
