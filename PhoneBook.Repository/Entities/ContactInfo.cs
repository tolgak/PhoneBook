using System;
using System.Text.Json.Serialization;

namespace PhoneBook.Repository.Entities
{
  public class ContactInfo
  {
    public Guid Id { get; set; }

    public Guid PersonId { get; set; }

    [JsonIgnore]
    public Person Person { get; set; }

    public int ContactInfoType { get; set; }
    public string Info { get; set; }
  }
}
