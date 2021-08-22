using System;
using System.Collections.Generic;

namespace MockDataGenerator
{
  public class Person
  {
    public Guid Id { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Company { get; set; }

    public IEnumerable<ContactInfo> ContactInfos { get; set; }
  }
}
