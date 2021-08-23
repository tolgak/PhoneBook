using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PhoneBook.Repository.Entities
{
  public class Person
  {
    public Guid Id { get; set; }
    
    public string First_Name { get; set; }

    public string Last_Name { get; set; }
    public string Company { get; set; }
    public List<ContactInfo> ContactInfos { get; set; }
  }
}
