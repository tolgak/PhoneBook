using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator
{
  public class ContactInfo
  {
    public Guid Id { get; set; }

    public Guid PersonId { get; set; }

    public int ContactInfoType { get; set; }
    public string Info { get; set; }
  }

}
