using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhoneBook.Repository.Entities
{
  public class ContactInfo
  {
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid PersonId { get; set; }

    [JsonIgnore]
    public Person Person { get; set; }

    public int ContactInfoType { get; set; }
    public string Info { get; set; }
  }
}
