using System;
using System.Collections.Generic;

namespace PhoneBook.Dto
{
  public class dtoPerson
  {
    public Guid Id { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Company { get; set; }
    public List<dtoContactInfo> ContactInfos{ get; set; }
  }


}
